namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdOnLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FamilyMembersLinks", "Child_AnimalId", "dbo.Animals");
            DropForeignKey("dbo.FamilyMembersLinks", "Parent_AnimalId", "dbo.Animals");
            DropIndex("dbo.FamilyMembersLinks", new[] { "Child_AnimalId" });
            DropIndex("dbo.FamilyMembersLinks", new[] { "Parent_AnimalId" });
            AddColumn("dbo.FamilyMembersLinks", "ParentId", c => c.Int(nullable: false));
            AddColumn("dbo.FamilyMembersLinks", "ChildId", c => c.Int(nullable: false));
            DropColumn("dbo.FamilyMembersLinks", "Child_AnimalId");
            DropColumn("dbo.FamilyMembersLinks", "Parent_AnimalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FamilyMembersLinks", "Parent_AnimalId", c => c.Int());
            AddColumn("dbo.FamilyMembersLinks", "Child_AnimalId", c => c.Int());
            DropColumn("dbo.FamilyMembersLinks", "ChildId");
            DropColumn("dbo.FamilyMembersLinks", "ParentId");
            CreateIndex("dbo.FamilyMembersLinks", "Parent_AnimalId");
            CreateIndex("dbo.FamilyMembersLinks", "Child_AnimalId");
            AddForeignKey("dbo.FamilyMembersLinks", "Parent_AnimalId", "dbo.Animals", "AnimalId");
            AddForeignKey("dbo.FamilyMembersLinks", "Child_AnimalId", "dbo.Animals", "AnimalId");
        }
    }
}
