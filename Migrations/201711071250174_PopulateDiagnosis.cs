namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDiagnosis : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Diagnosis (Beskrivning) " +
                "values ('F�rkyld'), ('Uppbl�st mage'), ('Tarmbesv�r')");
        }
        
        public override void Down()
        {
        }
    }
}
