namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FamilyMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FamilyMembersLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Child_AnimalId = c.Int(),
                        Parent_AnimalId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.Child_AnimalId)
                .ForeignKey("dbo.Animals", t => t.Parent_AnimalId)
                .Index(t => t.Child_AnimalId)
                .Index(t => t.Parent_AnimalId);
            
            AddColumn("dbo.Animals", "Animal_AnimalId", c => c.Int());
            CreateIndex("dbo.Animals", "Animal_AnimalId");
            AddForeignKey("dbo.Animals", "Animal_AnimalId", "dbo.Animals", "AnimalId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FamilyMembersLinks", "Parent_AnimalId", "dbo.Animals");
            DropForeignKey("dbo.FamilyMembersLinks", "Child_AnimalId", "dbo.Animals");
            DropForeignKey("dbo.Animals", "Animal_AnimalId", "dbo.Animals");
            DropIndex("dbo.FamilyMembersLinks", new[] { "Parent_AnimalId" });
            DropIndex("dbo.FamilyMembersLinks", new[] { "Child_AnimalId" });
            DropIndex("dbo.Animals", new[] { "Animal_AnimalId" });
            DropColumn("dbo.Animals", "Animal_AnimalId");
            DropTable("dbo.FamilyMembersLinks");
        }
    }
}
