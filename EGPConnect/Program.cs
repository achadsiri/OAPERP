using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EGPConnect.Models;
using EGPConnect.ApiModels;
using EGPConnect.Utility;


class Program
{
    static async Task Main(string[] args)
    {
        await Run();
    }

    static async Task Run()
    {

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        string? connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<OAPDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var db = new OAPDbContext(optionsBuilder.Options);

        // ตัวอย่างการใช้งาน
        string? apiKey = db.SystemConfigs.FirstOrDefault(x => x.ConfigName == "APIKey")?.ConfigStringValue;
        string? deptCode = db.SystemConfigs.FirstOrDefault(x => x.ConfigName == "dept_code")?.ConfigStringValue;
        string? summary_cgdcontract = db.SystemConfigs.FirstOrDefault(x => x.ConfigName == "summary_cgdcontract")?.ConfigStringValue;
        string? cgdcontract = db.SystemConfigs.FirstOrDefault(x => x.ConfigName == "cgdcontract")?.ConfigStringValue;
                

        using HttpClient client = new HttpClient();
        try
        {
            // 📊 1. Summary API
            string summaryUrl = summary_cgdcontract!.Replace("{api-key}", apiKey).Replace("{dept_code}", deptCode);
            string summaryJson = await client.GetStringAsync(summaryUrl);
            var summaryResult = JsonSerializer.Deserialize<RootSummary>(summaryJson);
            int summary_total_project = summaryResult?.summary.total_project ?? 0;
            Console.WriteLine("📊 Summary:");
            Console.WriteLine($"Total Projects: {summaryResult?.summary.total_project}");
            Console.WriteLine($"Total Price   : {summaryResult?.summary.total_price:N2} Baht");

            // 📋 2. Detail API
            string detailUrl = cgdcontract!.Replace("{api-key}", apiKey).Replace("{dept_code}", deptCode).Replace("{total_project}", summary_total_project.ToString());
            string detailJson = await client.GetStringAsync(detailUrl);
            //Console.WriteLine(detailJson); // ตรวจสอบว่าโครงสร้าง JSON จริงเป็นอย่างไร

            // ตรวจสอบ JSON
            //Console.WriteLine("📥 Raw JSON:");
            //Console.WriteLine(detailJson.Substring(0, Math.Min(500, detailJson.Length)) + "...\n");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<RootDetail>(detailJson, options);

            Console.WriteLine($"\n📦 Raw result count: {(result?.result?.Count ?? 0)}");

            if (result?.result == null || result.result.Count == 0)
            {
                Console.WriteLine("❌ ไม่พบข้อมูลใน result.result");
                return;
            }

            /*
            Console.WriteLine("\n📋 Project Details:");
            foreach (var item in result.result)
            {
                Console.WriteLine($"📌 โครงการ: {item.project_name}");
                Console.WriteLine($"🧾 ประเภท: {item.project_type_name}");
                Console.WriteLine($"🏢 หน่วยงาน: {item.dept_name} ({item.dept_sub_name})");
                Console.WriteLine($"📅 ประกาศ: {item.announce_date}   ปีงบ: {item.budget_year}");
                Console.WriteLine($"💰 งบประมาณ: {item.project_money} บาท (ราคากลาง: {item.price_build})");
                Console.WriteLine($"📍 พื้นที่: {item.subdistrict}, {item.district}, {item.province}");
                Console.WriteLine($"📋 สถานะ: {item.project_status}");

                if (item.contract != null && item.contract.Count > 0)
                {
                    foreach (var c in item.contract)
                    {
                        Console.WriteLine($"    ✅ ผู้ชนะ: {c.winner} (TIN: {c.winner_tin})");
                        Console.WriteLine($"    📑 สัญญาเลขที่: {c.contract_no} เริ่ม: {c.contract_date} - สิ้นสุด: {c.contract_finish_date}");
                        Console.WriteLine($"    💵 ราคาตกลง: {c.price_agree} บาท | สถานะ: {c.status}");
                    }
                }

                Console.WriteLine(new string('-', 80));
            }
            */

            EGPConnect.Services.ProjectService.SaveOrUpdateAllProjects(db, result.result, 2568, 1);
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ ERROR: " + ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine("🔍 INNER: " + ex.InnerException.Message);
        }
    }
}








