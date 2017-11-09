namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFKOnDescriptionId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Descriptions", new[] { "DescriptionId" });
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            CreateIndex("dbo.Descriptions", "DescriptionId");
            DropColumn("dbo.VetVisits", "DescriptionId");
            DropColumn("dbo.Descriptions", "VetVisitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Descriptions", "VetVisitId", c => c.Int(nullable: false));
            AddColumn("dbo.VetVisits", "DescriptionId", c => c.Int(nullable: false));
            DropIndex("dbo.Descriptions", new[] { "DescriptionId" });
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            CreateIndex("dbo.Descriptions", "DescriptionId");
        }
    }
}
