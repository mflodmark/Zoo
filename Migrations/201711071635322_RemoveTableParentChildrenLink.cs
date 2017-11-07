namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTableParentChildrenLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ParentChildrenLink", "ParentId", "dbo.Animals");
            DropForeignKey("dbo.ParentChildrenLink", "ChildId", "dbo.Animals");
            DropIndex("dbo.ParentChildrenLink", new[] { "ParentId" });
            DropIndex("dbo.ParentChildrenLink", new[] { "ChildId" });
            DropTable("dbo.ParentChildrenLink");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ParentChildrenLink",
                c => new
                    {
                        Animal_AnimalId = c.Int(nullable: false),
                        Animal_AnimalId1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Animal_AnimalId, t.Animal_AnimalId1 });
            
            CreateIndex("dbo.ParentChildrenLink", "ParentId");
            CreateIndex("dbo.ParentChildrenLink", "ChildId");
            AddForeignKey("dbo.ParentChildrenLink", "ParentId", "dbo.Animals", "AnimalId");
            AddForeignKey("dbo.ParentChildrenLink", "ChildId", "dbo.Animals", "AnimalId");
        }
    }
}
