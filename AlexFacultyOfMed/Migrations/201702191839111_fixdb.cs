namespace AlexFacultyOfMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdb : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.DailyFiles", name: "IX_DailyId", newName: "DailyIdIndex");
            //AddColumn("dbo.DailyFiles", "EmployeesCashNumber", c => c.Int());
            //AddColumn("dbo.DailyFiles", "FileTotalCashAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DailyFileDetails", "IsAtm", c => c.Boolean(nullable: false));
            //AlterColumn("dbo.DailyFiles", "EmployeesNumber", c => c.Int());
            //AlterColumn("dbo.DailyFiles", "FileTotalAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.DailyFiles", "FileTotalAmount", c => c.Decimal(precision: 18, scale: 2));
            //AlterColumn("dbo.DailyFiles", "EmployeesNumber", c => c.Int());
            DropColumn("dbo.DailyFileDetails", "IsAtm");
            //DropColumn("dbo.DailyFiles", "FileTotalCashAmount");
            //DropColumn("dbo.DailyFiles", "EmployeesCashNumber");
            RenameIndex(table: "dbo.DailyFiles", name: "DailyIdIndex", newName: "IX_DailyId");
        }
    }
}
