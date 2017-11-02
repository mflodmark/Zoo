namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmendDatatypeOnEnviromentName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enviroments", "Name", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enviroments", "Name", c => c.String(nullable: false));
        }
    }
}
