namespace EPayment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                        BankName = c.String(),
                        BankBrnchName = c.String(),
                        AccountNumber = c.String(),
                        TaxNumber = c.String(),
                        RecordTradingNumber = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Dailies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TotalAmount = c.Decimal(precision: 18, scale: 2),
                        CheckGP = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DailyDay = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        ExpensessTypeId = c.Int(nullable: false),
                        FilePath = c.String(),
                        Open = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpensessTypes", t => t.ExpensessTypeId, cascadeDelete: true)
                .Index(t => t.ExpensessTypeId);
            
            CreateTable(
                "dbo.DailyFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DataEntryName = c.String(),
                        FileNumberInfo = c.String(),
                        EmployeesNumber = c.Int(),
                        FileTotalAmount = c.Decimal(precision: 18, scale: 2),
                        FilePath = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DailyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dailies", t => t.DailyId, cascadeDelete: true)
                .Index(t => t.DailyId);
            
            CreateTable(
                "dbo.DailyFileDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Net = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyFileId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.DailyFiles", t => t.DailyFileId, cascadeDelete: true)
                .Index(t => t.DailyFileId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Nickname = c.String(),
                        Gender = c.Int(),
                        NationalId = c.String(),
                        PositionName = c.String(),
                        DepartmentId = c.Int(),
                        Phone = c.String(),
                        Email = c.String(),
                        Sallary = c.Boolean(nullable: false),
                        Other = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpensessTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orgnizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OrgnizationNumber = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            */
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Code = c.Int(nullable: false),
                        NationalId = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserLogins", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserClaims", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");

            DropForeignKey("dbo.Dailies", "ExpensessTypeId", "dbo.ExpensessTypes");
            DropForeignKey("dbo.DailyFiles", "DailyId", "dbo.Dailies");
            DropForeignKey("dbo.DailyFileDetails", "DailyFileId", "dbo.DailyFiles");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DailyFileDetails", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CompanyBranches", "CompanyId", "dbo.Companies");
            DropIndex("dbo.IdentityUserLogins", new[] { "AspNetUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "AspNetUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "AspNetUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.DailyFileDetails", new[] { "EmployeeId" });
            DropIndex("dbo.DailyFileDetails", new[] { "DailyFileId" });
            DropIndex("dbo.DailyFiles", new[] { "DailyId" });
            DropIndex("dbo.Dailies", new[] { "ExpensessTypeId" });
            DropIndex("dbo.CompanyBranches", new[] { "CompanyId" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Orgnizations");
            DropTable("dbo.ExpensessTypes");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.DailyFileDetails");
            DropTable("dbo.DailyFiles");
            DropTable("dbo.Dailies");
            DropTable("dbo.CompanyBranches");
            DropTable("dbo.Companies");
        }
    }
}
