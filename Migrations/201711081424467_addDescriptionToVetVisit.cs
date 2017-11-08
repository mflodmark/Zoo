namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDescriptionToVetVisit : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Descriptions");
            AddColumn("dbo.VetVisits", "DescriptionId", c => c.Int(nullable: false));
            AddColumn("dbo.Descriptions", "VetVisitId", c => c.Int(nullable: false));
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Descriptions", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            CreateIndex("dbo.Descriptions", "DescriptionId");
            AddForeignKey("dbo.Descriptions", "DescriptionId", "dbo.VetVisits", "VetVisitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Descriptions", "DescriptionId", "dbo.VetVisits");
            DropIndex("dbo.Descriptions", new[] { "DescriptionId" });
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "Name", c => c.String());
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Descriptions", "VetVisitId");
            DropColumn("dbo.VetVisits", "DescriptionId");
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
        }
    }
}
