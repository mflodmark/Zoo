namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVetVisit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VetVisits",
                c => new
                    {
                        VetVisitId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        VetId = c.Int(nullable: false),
                        AnimalId = c.Int(nullable: false),
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
                        Beskrivning = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisId);
            
            CreateTable(
                "dbo.Vets",
                c => new
                    {
                        VetId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VetVisits", "VetId", "dbo.Vets");
            DropForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.VetVisits", "AnimalId", "dbo.Animals");
            DropIndex("dbo.VetVisits", new[] { "DiagnosisId" });
            DropIndex("dbo.VetVisits", new[] { "AnimalId" });
            DropIndex("dbo.VetVisits", new[] { "VetId" });
            DropTable("dbo.Vets");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.VetVisits");
        }
    }
}
