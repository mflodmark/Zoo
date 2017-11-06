namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenderToAnimal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenderId);
            
            AddColumn("dbo.Animals", "GenderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Animals", "GenderId");
            AddForeignKey("dbo.Animals", "GenderId", "dbo.Genders", "GenderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "GenderId", "dbo.Genders");
            DropIndex("dbo.Animals", new[] { "GenderId" });
            DropColumn("dbo.Animals", "GenderId");
            DropTable("dbo.Genders");
        }
    }
}
