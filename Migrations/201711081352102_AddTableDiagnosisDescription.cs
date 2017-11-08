namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDiagnosisDescription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiagnosisDescriptions",
                c => new
                    {
                        DiagnosisDescriptionId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DiagnosisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisDescriptionId)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId, cascadeDelete: true)
                .Index(t => t.DiagnosisId);
            
            DropColumn("dbo.Diagnosis", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Diagnosis", "Description", c => c.String());
            DropForeignKey("dbo.DiagnosisDescriptions", "DiagnosisId", "dbo.Diagnosis");
            DropIndex("dbo.DiagnosisDescriptions", new[] { "DiagnosisId" });
            DropTable("dbo.DiagnosisDescriptions");
        }
    }
}
