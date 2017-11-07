namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDateToDateAndTIme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VetVisits", "DateAndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.VetVisits", "Time");
            DropColumn("dbo.VetVisits", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VetVisits", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.VetVisits", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.VetVisits", "DateAndTime");
        }
    }
}
