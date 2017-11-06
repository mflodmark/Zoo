namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentChildrenLink",
                c => new
                    {
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new {t.ParentId, t.ChildId })
                .ForeignKey("dbo.Animals", t => t.ParentId)
                .ForeignKey("dbo.Animals", t => t.ChildId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParentChildrenLink", "ChildId", "dbo.Animals");
            DropForeignKey("dbo.ParentChildrenLink", "ParentId", "dbo.Animals");
            DropIndex("dbo.ParentChildrenLink", new[] { "ChildId" });
            DropIndex("dbo.ParentChildrenLink", new[] { "ParentId" });
            DropTable("dbo.ParentChildrenLink");
        }
    }
}
