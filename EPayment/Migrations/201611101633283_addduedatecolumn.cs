namespace EPayment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addduedatecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dailies", "dueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dailies", "dueDate");
        }
    }
}
