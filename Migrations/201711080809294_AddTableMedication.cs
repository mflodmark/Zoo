namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableMedication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.MedicationDiagnosis",
                c => new
                    {
                        Medication_MedicationId = c.Int(nullable: false),
                        Diagnosis_DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medication_MedicationId, t.Diagnosis_DiagnosisId })
                .ForeignKey("dbo.Medications", t => t.Medication_MedicationId, cascadeDelete: true)
                .ForeignKey("dbo.Diagnosis", t => t.Diagnosis_DiagnosisId, cascadeDelete: true)
                .Index(t => t.Medication_MedicationId)
                .Index(t => t.Diagnosis_DiagnosisId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicationDiagnosis", "Diagnosis_DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.MedicationDiagnosis", "Medication_MedicationId", "dbo.Medications");
            DropIndex("dbo.MedicationDiagnosis", new[] { "Diagnosis_DiagnosisId" });
            DropIndex("dbo.MedicationDiagnosis", new[] { "Medication_MedicationId" });
            DropTable("dbo.MedicationDiagnosis");
            DropTable("dbo.Medications");
        }
    }
}
