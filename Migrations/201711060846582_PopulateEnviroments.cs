namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateEnviroments : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Enviroments (Name) " +
                "values ('Mark'), ('Träd'), ('Vatten')");
        }
        
        public override void Down()
        {
        }
    }
}
