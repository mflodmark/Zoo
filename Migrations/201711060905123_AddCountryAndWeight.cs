namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryAndWeight : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryOfOrigins",
                c => new
                    {
                        CountryOfOriginId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryOfOriginId);
            
            AddColumn("dbo.Animals", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.Animals", "CountryOfOriginId", c => c.Int(nullable: false));
            CreateIndex("dbo.Animals", "CountryOfOriginId");
            AddForeignKey("dbo.Animals", "CountryOfOriginId", "dbo.CountryOfOrigins", "CountryOfOriginId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "CountryOfOriginId", "dbo.CountryOfOrigins");
            DropIndex("dbo.Animals", new[] { "CountryOfOriginId" });
            DropColumn("dbo.Animals", "CountryOfOriginId");
            DropColumn("dbo.Animals", "Weight");
            DropTable("dbo.CountryOfOrigins");
        }
    }
}
