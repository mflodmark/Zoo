namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMedication : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Medications (name) values ('Magmedicin'), ('H�stmedicin'), ('Antibiotika'), ('Kr�kmedicin')");
        }
        
        public override void Down()
        {
        }
    }
}
