namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDiagnosis : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Diagnosis (Beskrivning) " +
                "values ('Förkyld'), ('Uppblåst mage'), ('Tarmbesvär')");
        }
        
        public override void Down()
        {
        }
    }
}
