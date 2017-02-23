namespace AlexFacultyOfMed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLoginDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLoginDate");
            DropColumn("dbo.AspNetUsers", "IsOnline");
        }
    }
}
