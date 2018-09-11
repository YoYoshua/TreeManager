namespace TreeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Nodes", name: "ParentNode_NodeID", newName: "ParentID");
            RenameIndex(table: "dbo.Nodes", name: "IX_ParentNode_NodeID", newName: "IX_ParentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Nodes", name: "IX_ParentID", newName: "IX_ParentNode_NodeID");
            RenameColumn(table: "dbo.Nodes", name: "ParentID", newName: "ParentNode_NodeID");
        }
    }
}
