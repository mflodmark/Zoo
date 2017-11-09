namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFluentAPIDesc : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Descriptions", new[] { "DescriptionId" });
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            CreateIndex("dbo.Descriptions", "DescriptionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Descriptions", new[] { "DescriptionId" });
            DropPrimaryKey("dbo.Descriptions");
            AlterColumn("dbo.Descriptions", "DescriptionId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Descriptions", "DescriptionId");
            CreateIndex("dbo.Descriptions", "DescriptionId");
        }
    }
}
