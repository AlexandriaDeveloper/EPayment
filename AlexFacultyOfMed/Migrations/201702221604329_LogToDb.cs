namespace AlexFacultyOfMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogDate = c.DateTime(nullable: false),
                        ExceptionMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
