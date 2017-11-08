namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropLinkClassParents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnosis", "Name", c => c.Int(nullable: false));
            DropTable("dbo.FamilyMembersLinks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FamilyMembersLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Diagnosis", "Name");
        }
    }
}
