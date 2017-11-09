namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsUsedToVetVisit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VetVisits", "IsUsed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VetVisits", "IsUsed");
        }
    }
}
