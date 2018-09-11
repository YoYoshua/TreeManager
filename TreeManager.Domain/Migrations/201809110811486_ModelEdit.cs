namespace TreeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelEdit : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Nodes", name: "ParentID", newName: "Parent_NodeID");
            RenameIndex(table: "dbo.Nodes", name: "IX_ParentID", newName: "IX_Parent_NodeID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Nodes", name: "IX_Parent_NodeID", newName: "IX_ParentID");
            RenameColumn(table: "dbo.Nodes", name: "Parent_NodeID", newName: "ParentID");
        }
    }
}
