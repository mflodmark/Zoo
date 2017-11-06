namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGender : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genders (Name) values ('Male'), ('Female')");
        }
        
        public override void Down()
        {
        }
    }
}
