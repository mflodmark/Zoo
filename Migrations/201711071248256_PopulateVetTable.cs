namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateVetTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Vets (Name) " +
                "values ('Evidensia'), ('Anicura')");
        }
        
        public override void Down()
        {
        }
    }
}
