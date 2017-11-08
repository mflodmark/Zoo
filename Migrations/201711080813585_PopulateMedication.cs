namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMedication : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Medications (name) values ('Magmedicin'), ('Höstmedicin'), ('Antibiotika'), ('Kräkmedicin')");
        }
        
        public override void Down()
        {
        }
    }
}
