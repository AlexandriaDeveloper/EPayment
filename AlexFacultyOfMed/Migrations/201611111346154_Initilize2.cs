using System.Data.Entity.Migrations;

namespace AlexFacultyOfMed.Migrations
{
    public partial class Initilize2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dailies", "DueDate", c => c.DateTime());
        }

        public override void Down()
        {
            DropColumn("dbo.Dailies", "DueDate");
        }
    }
}