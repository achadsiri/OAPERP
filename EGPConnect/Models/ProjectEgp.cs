using System;
using System.Collections.Generic;

namespace EGPConnect.Models;

/// <summary>
/// ข้อมูลการจัดซื้อจัดจ้างจากภาครัฐ
/// </summary>
public partial class ProjectEgp
{
    /// <summary>
    /// รหัสอ้างอิงที่ใช้ในระบบ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ผู้สร้าง อ้างอิง SystemUser.Id
    /// </summary>
    public int? CreateBy { get; set; }

    /// <summary>
    /// วันที่สร้าง
    /// </summary>
    public DateTime? CreateOn { get; set; }

    /// <summary>
    /// ผู้แก้ไข อ้างอิง SystemUser.Id
    /// </summary>
    public int? UpdateBy { get; set; }

    /// <summary>
    /// วันที่แก้ไข
    /// </summary>
    public DateTime? UpdateOn { get; set; }

    /// <summary>
    /// เลขที่อ้างอิง จาก project_id ของ API
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// ปีงบประมาณ
    /// </summary>
    public int? BudgetYear { get; set; }

    /// <summary>
    /// หน่วยงาน อ้างอิง MasterOrganization.Id 
    /// </summary>
    public int? OrganizationId { get; set; }

    /// <summary>
    /// รหัสโครงการ Project_Id
    /// </summary>
    public string? ProjectId { get; set; }

    /// <summary>
    /// ชื่อโครงการที่จัดซื้อจัดจ้าง
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// ชื่อประเภทโครงการ
    /// </summary>
    public string? ProjectTypeName { get; set; }

    /// <summary>
    /// ชื่อหน่วยงาน
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// ชื่อหน่วยงานย่อย
    /// </summary>
    public string? DeptSubName { get; set; }

    /// <summary>
    /// ชื่อวิธีการจัดซื้อจัดจ้าง
    /// </summary>
    public string? PurchaseMethodName { get; set; }

    /// <summary>
    /// ชื่อกลุ่มวิธีการจัดซื้อจัดจ้าง
    /// </summary>
    public string? PurchaseMethodGroupName { get; set; }

    /// <summary>
    /// วันที่ประกาศจัดซื้อจัดจ้าง
    /// </summary>
    public DateTime? AnnounceDate { get; set; }

    /// <summary>
    /// วงเงินงบประมาณ
    /// </summary>
    public decimal? ProjectMoney { get; set; }

    /// <summary>
    /// ราคากลาง
    /// </summary>
    public decimal? PriceBuild { get; set; }

    /// <summary>
    /// ราคาที่ตกลงซื้อ / จ้าง ซึ่งรวมทุกสัญญาในโครงการ
    /// </summary>
    public decimal? SumPriceAgree { get; set; }

    /// <summary>
    /// ปีงบประมาณ
    /// </summary>
    public int? BudgetYear1 { get; set; }

    /// <summary>
    /// วันที่เกิดรายการ
    /// </summary>
    public string? TransactionDate { get; set; }

    /// <summary>
    /// ชื่อจังหวัด
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// ชื่อเขต / อำเภอ
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// ชื่อแขวง / ตำบล
    /// </summary>
    public string? Subdistrict { get; set; }

    /// <summary>
    /// สถานะโครงการ
    /// </summary>
    public string? ProjectStatus { get; set; }

    /// <summary>
    /// พิกัดละติจูดของโครงการ (ถ้าไม่มีจะส่งเป็น 0)
    /// </summary>
    public decimal? Lat { get; set; }

    /// <summary>
    /// พิกัดลองจิจูดของโครงการ (ถ้าไม่มีจะส่งเป็น 0)
    /// </summary>
    public decimal? Lon { get; set; }

    /// <summary>
    /// พิกัดของโครงการ
    /// </summary>
    public string? Geom { get; set; }

    public virtual ICollection<ProjectEgpcontract> ProjectEgpcontracts { get; set; } = new List<ProjectEgpcontract>();
}
