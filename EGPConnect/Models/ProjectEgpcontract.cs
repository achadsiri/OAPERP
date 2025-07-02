using System;
using System.Collections.Generic;

namespace EGPConnect.Models;

/// <summary>
/// รายการสัญญา
/// </summary>
public partial class ProjectEgpcontract
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
    /// รหัสโครงการ อ้างอิง ProjectEGD.Id
    /// </summary>
    public int? ProjectEgpid { get; set; }

    /// <summary>
    /// รหัสโครงการ Project_Id
    /// </summary>
    public string? ProjectId { get; set; }

    /// <summary>
    /// เลขประจำตัวนิติบุคคล 13 หลัก
    /// </summary>
    public string? WinnerTin { get; set; }

    /// <summary>
    /// ชื่อบริษัทผู้ชนะการเสนอราคา
    /// </summary>
    public string? Winner { get; set; }

    /// <summary>
    /// เลขที่สัญญา
    /// </summary>
    public string? ContractNo { get; set; }

    /// <summary>
    /// วันที่ลงนามในสัญญา
    /// </summary>
    public DateTime? ContractDate { get; set; }

    /// <summary>
    /// วันที่สิ้นสุดสัญญา
    /// </summary>
    public DateTime? ContractFinishDate { get; set; }

    /// <summary>
    /// วงเงินงบประมาณ
    /// </summary>
    public decimal? PriceAgree { get; set; }

    /// <summary>
    /// สถานะสัญญา
    /// </summary>
    public string? Status { get; set; }

    public virtual ProjectEgp? ProjectEgp { get; set; }
}
