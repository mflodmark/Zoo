namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Types (Name) " +
                "values ('Köttätare'), ('Växtätare')");

            Sql("Insert into Enviroments (Name) " +
                "values ('Mark'), ('Träd'), ('Vatten')");

            Sql("Insert into Genders (Name) values ('Male'), ('Female')");

            Sql("Insert into CountryOfOrigins (Name) " +
                "values ('Sverige'), ('Norge'), ('Danmark'), ('Finland'), ('Island')");

            Sql("Insert into Diagnosis (Name) " +
                "values ('Förkyld'), ('Uppblåst mage'), ('Tarmbesvär')");

            Sql("insert into Medications (name) values ('Magmedicin'), ('Höstmedicin'), ('Antibiotika'), ('Kräkmedicin')");

            Sql("insert into vets (name) values ('Evidensia'), ('Djurakuten')");

        }

        public override void Down()
        {
        }
    }
}
