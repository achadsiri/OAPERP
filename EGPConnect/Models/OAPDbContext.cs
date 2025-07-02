using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EGPConnect.Models;

public partial class OAPDbContext : DbContext
{
    public OAPDbContext()
    {
    }

    public OAPDbContext(DbContextOptions<OAPDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProjectEgp> ProjectEgps { get; set; }

    public virtual DbSet<ProjectEgpcontract> ProjectEgpcontracts { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=dev.softsuite.co.th;Initial Catalog=OAPDB;User Id=oapadmin;Password=!oap2025!;TrustServerCertificate=True;");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEgp>(entity =>
        {
            entity.ToTable("ProjectEGP", tb => tb.HasComment("ข้อมูลการจัดซื้อจัดจ้างจากภาครัฐ"));

            entity.Property(e => e.Id).HasComment("รหัสอ้างอิงที่ใช้ในระบบ");
            entity.Property(e => e.AnnounceDate)
                .HasComment("วันที่ประกาศจัดซื้อจัดจ้าง")
                .HasColumnType("datetime")
                .HasColumnName("announce_date");
            entity.Property(e => e.BudgetYear).HasComment("ปีงบประมาณ");
            entity.Property(e => e.BudgetYear1)
                .HasComment("ปีงบประมาณ")
                .HasColumnName("budget_year");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("เลขที่อ้างอิง จาก project_id ของ API");
            entity.Property(e => e.CreateBy)
                .HasDefaultValue(-1)
                .HasComment("ผู้สร้าง อ้างอิง SystemUser.Id");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasComment("วันที่สร้าง")
                .HasColumnType("datetime");
            entity.Property(e => e.DeptName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("ชื่อหน่วยงาน")
                .HasColumnName("dept_name");
            entity.Property(e => e.DeptSubName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("ชื่อหน่วยงานย่อย")
                .HasColumnName("dept_sub_name");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("ชื่อเขต / อำเภอ")
                .HasColumnName("district");
            entity.Property(e => e.Geom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("พิกัดของโครงการ")
                .HasColumnName("geom");
            entity.Property(e => e.Lat)
                .HasComment("พิกัดละติจูดของโครงการ (ถ้าไม่มีจะส่งเป็น 0)")
                .HasColumnType("decimal(20, 15)")
                .HasColumnName("lat");
            entity.Property(e => e.Lon)
                .HasComment("พิกัดลองจิจูดของโครงการ (ถ้าไม่มีจะส่งเป็น 0)")
                .HasColumnType("decimal(20, 15)")
                .HasColumnName("lon");
            entity.Property(e => e.OrganizationId).HasComment("หน่วยงาน อ้างอิง MasterOrganization.Id ");
            entity.Property(e => e.PriceBuild)
                .HasComment("ราคากลาง")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("price_build");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("รหัสโครงการ Project_Id")
                .HasColumnName("project_id");
            entity.Property(e => e.ProjectMoney)
                .HasComment("วงเงินงบประมาณ")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("project_money");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("ชื่อโครงการที่จัดซื้อจัดจ้าง")
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("สถานะโครงการ")
                .HasColumnName("project_status");
            entity.Property(e => e.ProjectTypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("ชื่อประเภทโครงการ")
                .HasColumnName("project_type_name");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("ชื่อจังหวัด")
                .HasColumnName("province");
            entity.Property(e => e.PurchaseMethodGroupName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("ชื่อกลุ่มวิธีการจัดซื้อจัดจ้าง")
                .HasColumnName("purchase_method_group_name");
            entity.Property(e => e.PurchaseMethodName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("ชื่อวิธีการจัดซื้อจัดจ้าง")
                .HasColumnName("purchase_method_name");
            entity.Property(e => e.Subdistrict)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("ชื่อแขวง / ตำบล")
                .HasColumnName("subdistrict");
            entity.Property(e => e.SumPriceAgree)
                .HasComment("ราคาที่ตกลงซื้อ / จ้าง ซึ่งรวมทุกสัญญาในโครงการ")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("sum_price_agree");
            entity.Property(e => e.TransactionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("วันที่เกิดรายการ")
                .HasColumnName("transaction_date");
            entity.Property(e => e.UpdateBy).HasComment("ผู้แก้ไข อ้างอิง SystemUser.Id");
            entity.Property(e => e.UpdateOn)
                .HasComment("วันที่แก้ไข")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ProjectEgpcontract>(entity =>
        {
            entity.ToTable("ProjectEGPContract", tb => tb.HasComment("รายการสัญญา"));

            entity.HasIndex(e => e.ProjectEgpid, "IX_ProjectEGPContract");

            entity.HasIndex(e => e.ProjectId, "IX_ProjectEGPContract_1");

            entity.Property(e => e.Id).HasComment("รหัสอ้างอิงที่ใช้ในระบบ");
            entity.Property(e => e.ContractDate)
                .HasComment("วันที่ลงนามในสัญญา")
                .HasColumnType("datetime")
                .HasColumnName("contract_date");
            entity.Property(e => e.ContractFinishDate)
                .HasComment("วันที่สิ้นสุดสัญญา")
                .HasColumnType("datetime")
                .HasColumnName("contract_finish_date");
            entity.Property(e => e.ContractNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("เลขที่สัญญา")
                .HasColumnName("contract_no");
            entity.Property(e => e.CreateBy)
                .HasDefaultValue(-1)
                .HasComment("ผู้สร้าง อ้างอิง SystemUser.Id");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasComment("วันที่สร้าง")
                .HasColumnType("datetime");
            entity.Property(e => e.PriceAgree)
                .HasComment("วงเงินงบประมาณ")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("price_agree");
            entity.Property(e => e.ProjectEgpid)
                .HasComment("รหัสโครงการ อ้างอิง ProjectEGD.Id")
                .HasColumnName("ProjectEGPId");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("รหัสโครงการ Project_Id")
                .HasColumnName("project_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("สถานะสัญญา")
                .HasColumnName("status");
            entity.Property(e => e.UpdateBy).HasComment("ผู้แก้ไข อ้างอิง SystemUser.Id");
            entity.Property(e => e.UpdateOn)
                .HasComment("วันที่แก้ไข")
                .HasColumnType("datetime");
            entity.Property(e => e.Winner)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("ชื่อบริษัทผู้ชนะการเสนอราคา")
                .HasColumnName("winner");
            entity.Property(e => e.WinnerTin)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasComment("เลขประจำตัวนิติบุคคล 13 หลัก")
                .HasColumnName("winner_tin");

            entity.HasOne(d => d.ProjectEgp).WithMany(p => p.ProjectEgpcontracts)
                .HasForeignKey(d => d.ProjectEgpid)
                .HasConstraintName("FK_ProjectEGPContract_ProjectEGP");
        });

        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SystemConfig_1");

            entity.ToTable("SystemConfig", tb => tb.HasComment("การกำหนดค่าของระบบ"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("รหัสอ้างอิงที่ใช้ในระบบ");
            entity.Property(e => e.ConfigDoubleValue).HasComment("ค่าที่กำหนดในรูปแบบตัวเลขมีทศนิยม");
            entity.Property(e => e.ConfigIntValue).HasComment("ค่าที่กำหนดในรูปแบบตัวเลขจำนวนเต็ม");
            entity.Property(e => e.ConfigName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("ชื่อการกำหนดค่า");
            entity.Property(e => e.ConfigStringValue)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("ค่าที่กำหนดในรูปแบบตัวอักษร");
            entity.Property(e => e.ConfigType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("รูปแบบข้อมูล (I=Int, D=Double, S=String)");
            entity.Property(e => e.CreateBy)
                .HasDefaultValue(-1)
                .HasComment("ผู้สร้าง อ้างอิง SystemUser.Id");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasComment("วันที่สร้าง")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("รายละเอียด");
            entity.Property(e => e.UpdateBy).HasComment("ผู้แก้ไข อ้างอิง SystemUser.Id");
            entity.Property(e => e.UpdateOn)
                .HasComment("วันที่แก้ไข")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
