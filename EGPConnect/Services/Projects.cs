using EGPConnect.Models;
using EGPConnect.ApiModels;
using EGPConnect.Utility;
using Microsoft.EntityFrameworkCore;

namespace EGPConnect.Services
{
    public static class ProjectService
    {
        public static void SaveOrUpdateAllProjects(OAPDbContext db, List<ContractItem> items, int budgetYear, int organizationId)
        {
            using var transaction = db.Database.BeginTransaction();
            try
            {
                var projectIds = items.Select(x => x.project_id).ToList();

                // ดึงข้อมูลโครงการเดิมทั้งหมดจากฐานข้อมูลล่วงหน้า
                var existingProjects = db.ProjectEgps
                    .Where(p => projectIds.Contains(p.ProjectId))
                    .Include(p => p.ProjectEgpcontracts) // Include contracts
                    .ToList();

                foreach (var item in items)
                {
                    var project = existingProjects.FirstOrDefault(p => p.ProjectId == item.project_id);
                    bool isNew = false;

                    if (project == null)
                    {
                        project = new ProjectEgp
                        {
                            ProjectId = item.project_id,
                            Code = item.project_id,
                            BudgetYear = budgetYear,
                            OrganizationId = organizationId,
                            CreateBy = -1,
                            CreateOn = DateTime.Now
                        };
                        db.ProjectEgps.Add(project);
                        db.SaveChanges(); // เพื่อให้ได้ project.Id ก่อนใช้เป็น FK
                        isNew = true;

                        // Reload หลัง save เพื่อให้ EF ติดตาม correctly
                        project = db.ProjectEgps
                            .Include(p => p.ProjectEgpcontracts)
                            .First(p => p.ProjectId == item.project_id);
                    }

                    project.ProjectName = item.project_name;
                    project.ProjectTypeName = item.project_type_name;
                    project.DeptName = item.dept_name;
                    project.DeptSubName = item.dept_sub_name;
                    project.PurchaseMethodName = item.purchase_method_name;
                    project.PurchaseMethodGroupName = item.purchase_method_group_name;
                    project.AnnounceDate = ParserData.ParseThaiDate(item.announce_date);
                    project.ProjectMoney = ParserData.ParseDecimal(item.project_money);
                    project.PriceBuild = ParserData.ParseDecimal(item.price_build);
                    project.SumPriceAgree = ParserData.ParseDecimal(item.sum_price_agree);
                    project.BudgetYear1 = item.budget_year;
                    project.TransactionDate = item.transaction_date;
                    project.Province = item.province;
                    project.District = item.district;
                    project.Subdistrict = item.subdistrict;
                    project.ProjectStatus = item.project_status;
                    project.Lat = (decimal?)item.project_location?.lat;
                    project.Lon = (decimal?)item.project_location?.lon;
                    project.Geom = item.geom;
                    project.UpdateBy = -1;
                    project.UpdateOn = DateTime.Now;

                    // ลบสัญญาเดิม
                    if (project.ProjectEgpcontracts != null && project.ProjectEgpcontracts.Any())
                    {
                        db.ProjectEgpcontracts.RemoveRange(project.ProjectEgpcontracts);
                    }

                    // เพิ่มรายการสัญญาใหม่
                    if (item.contract != null)
                    {
                        foreach (var c in item.contract)
                        {
                            var contract = new ProjectEgpcontract
                            {
                                ProjectId = item.project_id,
                                ProjectEgpid = project.Id,
                                WinnerTin = c.winner_tin,
                                Winner = c.winner,
                                ContractNo = c.contract_no,
                                ContractDate = ParserData.ParseThaiDate(c.contract_date),
                                ContractFinishDate = ParserData.ParseThaiDate(c.contract_finish_date),
                                PriceAgree = ParserData.ParseDecimal(c.price_agree),
                                Status = c.status,
                                CreateBy = -1,
                                CreateOn = DateTime.Now,
                                UpdateBy = -1,
                                UpdateOn = DateTime.Now
                            };
                            db.ProjectEgpcontracts.Add(contract);
                        }
                    }
                }

                db.SaveChanges(); // ✅ Save รวมทั้ง Project และ Contract
                transaction.Commit(); // ✅ Commit เมื่อสำเร็จทั้งหมด
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // ❌ Rollback ถ้า error
                throw new Exception("เกิดข้อผิดพลาดระหว่างบันทึกข้อมูลโครงการและสัญญา", ex);
            }
        }
    }
}
