namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLinkFamily : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnimalAnimals", "Animal_AnimalId1", "dbo.Animals");
            DropForeignKey("dbo.AnimalAnimals", "Animal_AnimalId", "dbo.Animals");
            DropIndex("dbo.AnimalAnimals", new[] { "Animal_AnimalId1" });
            DropIndex("dbo.AnimalAnimals", new[] { "Animal_AnimalId" });
            DropTable("dbo.AnimalAnimals");
        }
    }
}
