namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCountryOfOrigin : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into CountryOfOrigins (Name) " +
                "values ('Sverige'), ('Norge'), ('Danmark'), ('Finland'), ('Island')");
        }
        
        public override void Down()
        {
        }
    }
}
