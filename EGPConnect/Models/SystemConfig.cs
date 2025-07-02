using System;
using System.Collections.Generic;

namespace EGPConnect.Models;

/// <summary>
/// การกำหนดค่าของระบบ
/// </summary>
public partial class SystemConfig
{
    /// <summary>
    /// รหัสอ้างอิงที่ใช้ในระบบ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ผู้สร้าง อ้างอิง SystemUser.Id
    /// </summary>
    public int CreateBy { get; set; }

    /// <summary>
    /// วันที่สร้าง
    /// </summary>
    public DateTime CreateOn { get; set; }

    /// <summary>
    /// ผู้แก้ไข อ้างอิง SystemUser.Id
    /// </summary>
    public int? UpdateBy { get; set; }

    /// <summary>
    /// วันที่แก้ไข
    /// </summary>
    public DateTime? UpdateOn { get; set; }

    /// <summary>
    /// ชื่อการกำหนดค่า
    /// </summary>
    public string ConfigName { get; set; } = null!;

    /// <summary>
    /// รูปแบบข้อมูล (I=Int, D=Double, S=String)
    /// </summary>
    public string ConfigType { get; set; } = null!;

    /// <summary>
    /// ค่าที่กำหนดในรูปแบบตัวอักษร
    /// </summary>
    public string? ConfigStringValue { get; set; }

    /// <summary>
    /// ค่าที่กำหนดในรูปแบบตัวเลขจำนวนเต็ม
    /// </summary>
    public int? ConfigIntValue { get; set; }

    /// <summary>
    /// ค่าที่กำหนดในรูปแบบตัวเลขมีทศนิยม
    /// </summary>
    public double? ConfigDoubleValue { get; set; }

    /// <summary>
    /// รายละเอียด
    /// </summary>
    public string? Description { get; set; }
}
