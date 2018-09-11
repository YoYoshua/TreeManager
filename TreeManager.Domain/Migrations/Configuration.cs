namespace TreeManager.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TreeManager.Domain.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TreeManager.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TreeManager.Domain.Concrete.EFDbContext context)
        {
            Node node1 = new Node()
            {
                NodeID = 1,
                Description = "Wêze³ testowy #1",
                Parent = null,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node1);
            context.SaveChanges();

            Node node2 = new Node()
            {
                NodeID = 2,
                Description = "Wêze³ testowy #2",
                Parent = node1,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node2);
            node1.ChildNodes.Add(node2);
            context.SaveChanges();

            Node node3 = new Node()
            {
                NodeID = 3,
                Description = "Wêze³ testowy #3",
                Parent = node1,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node3);
            node1.ChildNodes.Add(node3);
            context.SaveChanges();

            Node node4 = new Node()
            {
                NodeID = 4,
                Description = "Wêze³ testowy #4",
                Parent = null,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node4);
            context.SaveChanges();

            Node node5 = new Node()
            {
                NodeID = 5,
                Description = "Wêze³ testowy #5",
                Parent = node4,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node5);
            node4.ChildNodes.Add(node5);
            context.SaveChanges();

            Node node6 = new Node()
            {
                NodeID = 6,
                Description = "Wêze³ testowy #6",
                Parent = node5,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node6);
            node5.ChildNodes.Add(node6);
            context.SaveChanges();
        }
    }
}
