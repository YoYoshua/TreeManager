namespace TreeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        NodeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ParentNode_NodeID = c.Int(),
                    })
                .PrimaryKey(t => t.NodeID)
                .ForeignKey("dbo.Nodes", t => t.ParentNode_NodeID)
                .Index(t => t.ParentNode_NodeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nodes", "ParentNode_NodeID", "dbo.Nodes");
            DropIndex("dbo.Nodes", new[] { "ParentNode_NodeID" });
            DropTable("dbo.Nodes");
        }
    }
}
