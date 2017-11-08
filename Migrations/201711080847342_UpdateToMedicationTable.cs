namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToMedicationTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicationDiagnosis", "Medication_MedicationId", "dbo.Medications");
            DropForeignKey("dbo.MedicationDiagnosis", "Diagnosis_DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.MedicationDiagnosis", new[] { "Medication_MedicationId" });
            DropIndex("dbo.MedicationDiagnosis", new[] { "Diagnosis_DiagnosisId" });
            CreateTable(
                "dbo.MedicationVetVisits",
                c => new
                    {
                        Medication_MedicationId = c.Int(nullable: false),
                        VetVisit_VetVisitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medication_MedicationId, t.VetVisit_VetVisitId })
                .ForeignKey("dbo.Medications", t => t.Medication_MedicationId, cascadeDelete: true)
                .ForeignKey("dbo.VetVisits", t => t.VetVisit_VetVisitId, cascadeDelete: true)
                .Index(t => t.Medication_MedicationId)
                .Index(t => t.VetVisit_VetVisitId);
            
            DropTable("dbo.MedicationDiagnosis");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicationDiagnosis",
                c => new
                    {
                        Medication_MedicationId = c.Int(nullable: false),
                        Diagnosis_DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medication_MedicationId, t.Diagnosis_DiagnosisId });
            
            DropForeignKey("dbo.MedicationVetVisits", "VetVisit_VetVisitId", "dbo.VetVisits");
            DropForeignKey("dbo.MedicationVetVisits", "Medication_MedicationId", "dbo.Medications");
            DropIndex("dbo.MedicationVetVisits", new[] { "VetVisit_VetVisitId" });
            DropIndex("dbo.MedicationVetVisits", new[] { "Medication_MedicationId" });
            DropTable("dbo.MedicationVetVisits");
            CreateIndex("dbo.MedicationDiagnosis", "Diagnosis_DiagnosisId");
            CreateIndex("dbo.MedicationDiagnosis", "Medication_MedicationId");
            AddForeignKey("dbo.MedicationDiagnosis", "Diagnosis_DiagnosisId", "dbo.Diagnosis", "DiagnosisId", cascadeDelete: true);
            AddForeignKey("dbo.MedicationDiagnosis", "Medication_MedicationId", "dbo.Medications", "MedicationId", cascadeDelete: true);
        }
    }
}
