using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManager.Domain.Entities;

namespace TreeManager.Domain.Concrete
{
    //kontekst dla wezlow
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbContext") { }
        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new EFDBInit());
        }
        public DbSet<Node> Nodes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .HasOptional<Node>(n => n.Parent)
                .WithMany(n => n.ChildNodes)
                .WillCascadeOnDelete(false);
        }
    }

    public class EFDBInit : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(EFDbContext context)
        {
            Node node1 = new Node()
            {
                NodeID = 1,
                Title = "Węzeł #1",
                Description = "Węzeł testowy #1",
                Parent = null,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node1);
            context.SaveChanges();

            Node node2 = new Node()
            {
                NodeID = 2,
                Title = "Węzeł #2",
                Description = "Węzeł testowy #2",
                Parent = node1,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node2);
            node1.ChildNodes.Add(node2);
            context.SaveChanges();

            Node node3 = new Node()
            {
                NodeID = 3,
                Title = "Węzeł #3",
                Description = "Węzeł testowy #3",
                Parent = node1,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node3);
            node1.ChildNodes.Add(node3);
            context.SaveChanges();

            Node node4 = new Node()
            {
                NodeID = 4,
                Title = "Węzeł #4",
                Description = "Węzeł testowy #4",
                Parent = null,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node4);
            context.SaveChanges();

            Node node5 = new Node()
            {
                NodeID = 5,
                Title = "Węzeł #5",
                Description = "Węzeł testowy #5",
                Parent = node4,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node5);
            node4.ChildNodes.Add(node5);
            context.SaveChanges();

            Node node6 = new Node()
            {
                NodeID = 6,
                Title = "Węzeł #6",
                Description = "Węzeł testowy #6",
                Parent = node5,
                ChildNodes = new List<Node>()
            };
            context.Nodes.AddOrUpdate(node6);
            node5.ChildNodes.Add(node6);
            context.SaveChanges();
        }
    }

    //kontekst dla uzytkownikow
    public class AppIdentityDbContext : IdentityDbContext<User>
    {
        public AppIdentityDbContext() : base("EFDbContext") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    public class IdentityDbInit
        : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            //miejsce na poczatkowe instrukcje
        }
    }
}
