namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDescAndDiagnos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VetVisits", "DescriptionId", "dbo.Descriptions");
            DropForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.VetVisits", new[] { "DescriptionId" });
            DropIndex("dbo.VetVisits", new[] { "DiagnosisId" });
            AlterColumn("dbo.VetVisits", "DescriptionId", c => c.Int());
            AlterColumn("dbo.VetVisits", "DiagnosisId", c => c.Int());
            CreateIndex("dbo.VetVisits", "DescriptionId");
            CreateIndex("dbo.VetVisits", "DiagnosisId");
            AddForeignKey("dbo.VetVisits", "DescriptionId", "dbo.Descriptions", "DescriptionId");
            AddForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis", "DiagnosisId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.VetVisits", "DescriptionId", "dbo.Descriptions");
            DropIndex("dbo.VetVisits", new[] { "DiagnosisId" });
            DropIndex("dbo.VetVisits", new[] { "DescriptionId" });
            AlterColumn("dbo.VetVisits", "DiagnosisId", c => c.Int(nullable: false));
            AlterColumn("dbo.VetVisits", "DescriptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.VetVisits", "DiagnosisId");
            CreateIndex("dbo.VetVisits", "DescriptionId");
            AddForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis", "DiagnosisId", cascadeDelete: true);
            AddForeignKey("dbo.VetVisits", "DescriptionId", "dbo.Descriptions", "DescriptionId", cascadeDelete: true);
        }
    }
}
