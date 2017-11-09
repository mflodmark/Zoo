namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateVetVisit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Descriptions", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Descriptions", "DescriptionId", "dbo.VetVisits");
            DropIndex("dbo.Descriptions", new[] { "DescriptionId" });
            DropIndex("dbo.Descriptions", new[] { "DiagnosisId" });
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Descriptions", "Name", c => c.String());
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            DropColumn("dbo.Descriptions", "DiagnosisId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Descriptions", "DiagnosisId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            CreateIndex("dbo.Descriptions", "DiagnosisId");
            CreateIndex("dbo.Descriptions", "DescriptionId");
            AddForeignKey("dbo.Descriptions", "DescriptionId", "dbo.VetVisits", "VetVisitId");
            AddForeignKey("dbo.Descriptions", "DiagnosisId", "dbo.Diagnosis", "DiagnosisId", cascadeDelete: true);
        }
    }
}
