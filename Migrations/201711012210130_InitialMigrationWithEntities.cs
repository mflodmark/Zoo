namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationWithEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        AnimalId = c.Int(nullable: false, identity: true),
                        EnviromentId = c.Int(nullable: false),
                        SpeciesId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalId)
                .ForeignKey("dbo.Enviroments", t => t.EnviromentId, cascadeDelete: true)
                .ForeignKey("dbo.Species", t => t.SpeciesId, cascadeDelete: true)
                .Index(t => t.EnviromentId)
                .Index(t => t.SpeciesId);
            
            CreateTable(
                "dbo.Enviroments",
                c => new
                    {
                        EnviromentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EnviromentId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpeciesId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpeciesId)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Species", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Animals", "SpeciesId", "dbo.Species");
            DropForeignKey("dbo.Animals", "EnviromentId", "dbo.Enviroments");
            DropIndex("dbo.Species", new[] { "TypeId" });
            DropIndex("dbo.Animals", new[] { "SpeciesId" });
            DropIndex("dbo.Animals", new[] { "EnviromentId" });
            DropTable("dbo.Types");
            DropTable("dbo.Species");
            DropTable("dbo.Enviroments");
            DropTable("dbo.Animals");
        }
    }
}
