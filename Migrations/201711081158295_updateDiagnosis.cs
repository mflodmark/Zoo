namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnosis", "Namnet", c => c.String());
            DropColumn("dbo.Diagnosis", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Diagnosis", "Name", c => c.Int(nullable: false));
            DropColumn("dbo.Diagnosis", "Namnet");
        }
    }
}
