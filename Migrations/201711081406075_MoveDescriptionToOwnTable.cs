namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveDescriptionToOwnTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DiagnosisDescriptions", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.VetVisits", new[] { "DiagnosisId" });
            DropIndex("dbo.DiagnosisDescriptions", new[] { "DiagnosisId" });
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        DescriptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DescriptionId)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId, cascadeDelete: true)
                .Index(t => t.DiagnosisId);
            
            DropColumn("dbo.VetVisits", "DiagnosisId");
            DropTable("dbo.DiagnosisDescriptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DiagnosisDescriptions",
                c => new
                    {
                        DiagnosisDescriptionId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisDescriptionId);
            
            AddColumn("dbo.VetVisits", "DiagnosisId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Descriptions", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.Descriptions", new[] { "DiagnosisId" });
            DropTable("dbo.Descriptions");
            CreateIndex("dbo.DiagnosisDescriptions", "DiagnosisId");
            CreateIndex("dbo.VetVisits", "DiagnosisId");
            AddForeignKey("dbo.VetVisits", "DiagnosisId", "dbo.Diagnosis", "DiagnosisId", cascadeDelete: true);
            AddForeignKey("dbo.DiagnosisDescriptions", "DiagnosisId", "dbo.Diagnosis", "DiagnosisId", cascadeDelete: true);
        }
    }
}
