using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public DbSet<Node> Nodes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .HasOptional<Node>(n => n.Parent)
                .WithMany(n => n.ChildNodes)
                .WillCascadeOnDelete(false);
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
