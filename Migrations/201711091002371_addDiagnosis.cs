namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VetVisits", "DiagnosisId", c => c.Int(nullable: false));
            CreateIndex("dbo.VetVisits", "DiagnosisId");
            AddForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis", "DiagnosisId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.VetVisits", new[] { "DiagnosisId" });
            DropColumn("dbo.VetVisits", "DiagnosisId");
        }
    }
}
