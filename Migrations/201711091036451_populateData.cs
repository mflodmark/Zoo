namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Types (Name) " +
                "values ('K�tt�tare'), ('V�xt�tare')");

            Sql("Insert into Enviroments (Name) " +
                "values ('Mark'), ('Tr�d'), ('Vatten')");

            Sql("Insert into Genders (Name) values ('Male'), ('Female')");

            Sql("Insert into CountryOfOrigins (Name) " +
                "values ('Sverige'), ('Norge'), ('Danmark'), ('Finland'), ('Island')");

            Sql("Insert into Diagnosis (Name) " +
                "values ('F�rkyld'), ('Uppbl�st mage'), ('Tarmbesv�r')");

            Sql("insert into Medications (name) values ('Magmedicin'), ('H�stmedicin'), ('Antibiotika'), ('Kr�kmedicin')");

            Sql("insert into vets (name) values ('Evidensia'), ('Djurakuten')");

        }

        public override void Down()
        {
        }
    }
}
