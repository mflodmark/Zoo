namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VetVisits", "DescriptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.VetVisits", "DescriptionId");
            AddForeignKey("dbo.VetVisits", "DescriptionId", "dbo.Descriptions", "DescriptionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VetVisits", "DescriptionId", "dbo.Descriptions");
            DropIndex("dbo.VetVisits", new[] { "DescriptionId" });
            DropColumn("dbo.VetVisits", "DescriptionId");
        }
    }
}
