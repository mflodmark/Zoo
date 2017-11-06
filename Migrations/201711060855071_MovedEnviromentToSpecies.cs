namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedEnviromentToSpecies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Animals", "EnviromentId", "dbo.Enviroments");
            DropIndex("dbo.Animals", new[] { "EnviromentId" });
            AddColumn("dbo.Species", "EnviromentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Species", "EnviromentId");
            AddForeignKey("dbo.Species", "EnviromentId", "dbo.Enviroments", "EnviromentId", cascadeDelete: true);
            DropColumn("dbo.Animals", "EnviromentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "EnviromentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Species", "EnviromentId", "dbo.Enviroments");
            DropIndex("dbo.Species", new[] { "EnviromentId" });
            DropColumn("dbo.Species", "EnviromentId");
            CreateIndex("dbo.Animals", "EnviromentId");
            AddForeignKey("dbo.Animals", "EnviromentId", "dbo.Enviroments", "EnviromentId", cascadeDelete: true);
        }
    }
}
