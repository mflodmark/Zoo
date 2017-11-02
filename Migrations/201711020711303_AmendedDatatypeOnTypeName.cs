namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmendedDatatypeOnTypeName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Types", "Name", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Types", "Name", c => c.String(nullable: false));
        }
    }
}
