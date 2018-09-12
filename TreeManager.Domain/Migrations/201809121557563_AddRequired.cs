namespace TreeManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nodes", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nodes", "Title", c => c.String());
        }
    }
}
