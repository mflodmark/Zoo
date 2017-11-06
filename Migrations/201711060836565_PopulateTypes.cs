namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTypes : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Types (Name) " +
                "values ('Köttätare'), ('Växtätare')");
        }
        
        public override void Down()
        {
        }
    }
}
