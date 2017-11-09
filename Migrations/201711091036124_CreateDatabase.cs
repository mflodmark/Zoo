namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        AnimalId = c.Int(nullable: false, identity: true),
                        SpeciesId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Weight = c.Double(nullable: false),
                        CountryOfOriginId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalId)
                .ForeignKey("dbo.CountryOfOrigins", t => t.CountryOfOriginId, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .ForeignKey("dbo.Species", t => t.SpeciesId, cascadeDelete: true)
                .Index(t => t.SpeciesId)
                .Index(t => t.CountryOfOriginId)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.CountryOfOrigins",
                c => new
                    {
                        CountryOfOriginId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryOfOriginId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpeciesId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TypeId = c.Int(nullable: false),
                        EnviromentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpeciesId)
                .ForeignKey("dbo.Enviroments", t => t.EnviromentId, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.EnviromentId);
            
            CreateTable(
                "dbo.Enviroments",
                c => new
                    {
                        EnviromentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EnviromentId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.VetVisits",
                c => new
                    {
                        VetVisitId = c.Int(nullable: false, identity: true),
                        DateAndTime = c.DateTime(nullable: false),
                        VetId = c.Int(nullable: false),
                        AnimalId = c.Int(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                        DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VetVisitId)
                .ForeignKey("dbo.Animals", t => t.AnimalId, cascadeDelete: true)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId, cascadeDelete: true)
                .ForeignKey("dbo.Vets", t => t.VetId, cascadeDelete: true)
                .Index(t => t.VetId)
                .Index(t => t.AnimalId)
                .Index(t => t.DiagnosisId);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.Vets",
                c => new
                    {
                        VetId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VetId);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        DescriptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DescriptionId);
            
            CreateTable(
                "dbo.AnimalAnimals",
                c => new
                    {
                        Animal_AnimalId = c.Int(nullable: false),
                        Animal_AnimalId1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Animal_AnimalId, t.Animal_AnimalId1 })
                .ForeignKey("dbo.Animals", t => t.Animal_AnimalId)
                .ForeignKey("dbo.Animals", t => t.Animal_AnimalId1)
                .Index(t => t.Animal_AnimalId)
                .Index(t => t.Animal_AnimalId1);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VetVisits", "VetId", "dbo.Vets");
            DropForeignKey("dbo.MedicationVetVisits", "VetVisit_VetVisitId", "dbo.VetVisits");
            DropForeignKey("dbo.MedicationVetVisits", "Medication_MedicationId", "dbo.Medications");
            DropForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.VetVisits", "AnimalId", "dbo.Animals");
            DropForeignKey("dbo.Species", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Species", "EnviromentId", "dbo.Enviroments");
            DropForeignKey("dbo.Animals", "SpeciesId", "dbo.Species");
            DropForeignKey("dbo.Animals", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Animals", "CountryOfOriginId", "dbo.CountryOfOrigins");
            DropForeignKey("dbo.AnimalAnimals", "Animal_AnimalId1", "dbo.Animals");
            DropForeignKey("dbo.AnimalAnimals", "Animal_AnimalId", "dbo.Animals");
            DropIndex("dbo.MedicationVetVisits", new[] { "VetVisit_VetVisitId" });
            DropIndex("dbo.MedicationVetVisits", new[] { "Medication_MedicationId" });
            DropIndex("dbo.AnimalAnimals", new[] { "Animal_AnimalId1" });
            DropIndex("dbo.AnimalAnimals", new[] { "Animal_AnimalId" });
            DropIndex("dbo.VetVisits", new[] { "DiagnosisId" });
            DropIndex("dbo.VetVisits", new[] { "AnimalId" });
            DropIndex("dbo.VetVisits", new[] { "VetId" });
            DropIndex("dbo.Species", new[] { "EnviromentId" });
            DropIndex("dbo.Species", new[] { "TypeId" });
            DropIndex("dbo.Animals", new[] { "GenderId" });
            DropIndex("dbo.Animals", new[] { "CountryOfOriginId" });
            DropIndex("dbo.Animals", new[] { "SpeciesId" });
            DropTable("dbo.MedicationVetVisits");
            DropTable("dbo.AnimalAnimals");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Vets");
            DropTable("dbo.Medications");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.VetVisits");
            DropTable("dbo.Types");
            DropTable("dbo.Enviroments");
            DropTable("dbo.Species");
            DropTable("dbo.Genders");
            DropTable("dbo.CountryOfOrigins");
            DropTable("dbo.Animals");
        }
    }
}
