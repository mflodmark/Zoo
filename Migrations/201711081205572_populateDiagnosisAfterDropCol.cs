namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateDiagnosisAfterDropCol : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Diagnosis (Name) " +
                "values ('Förkyld'), ('Uppblåst mage'), ('Tarmbesvär')");
        }
        
        public override void Down()
        {
        }
    }
}
