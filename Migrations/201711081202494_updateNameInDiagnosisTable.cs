namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNameInDiagnosisTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnosis", "Description", c => c.String());
            AddColumn("dbo.Diagnosis", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Diagnosis", "Namnet");
            DropColumn("dbo.Diagnosis", "Beskrivning");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Diagnosis", "Beskrivning", c => c.String(nullable: false));
            AddColumn("dbo.Diagnosis", "Namnet", c => c.String());
            DropColumn("dbo.Diagnosis", "Name");
            DropColumn("dbo.Diagnosis", "Description");
        }
    }
}
