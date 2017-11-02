namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameAndRemoveQuantityOnAnimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Animals", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Animals", "Name");
        }
    }
}
