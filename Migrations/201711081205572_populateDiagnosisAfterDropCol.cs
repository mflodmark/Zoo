namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateDiagnosisAfterDropCol : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Diagnosis (Name) " +
                "values ('F�rkyld'), ('Uppbl�st mage'), ('Tarmbesv�r')");
        }
        
        public override void Down()
        {
        }
    }
}
