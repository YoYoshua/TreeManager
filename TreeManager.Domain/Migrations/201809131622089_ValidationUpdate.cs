namespace TreeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nodes", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Nodes", "Description", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nodes", "Description", c => c.String());
            AlterColumn("dbo.Nodes", "Title", c => c.String(nullable: false));
        }
    }
}
