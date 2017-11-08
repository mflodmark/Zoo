namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLinkToFamilyUnderAnimal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Animals", "Animal_AnimalId", "dbo.Animals");
            DropIndex("dbo.Animals", new[] { "Animal_AnimalId" });
            DropColumn("dbo.Animals", "Animal_AnimalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "Animal_AnimalId", c => c.Int());
            CreateIndex("dbo.Animals", "Animal_AnimalId");
            AddForeignKey("dbo.Animals", "Animal_AnimalId", "dbo.Animals", "AnimalId");
        }
    }
}
