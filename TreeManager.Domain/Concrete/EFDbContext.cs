using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManager.Domain.Entities;

namespace TreeManager.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .HasOptional<Node>(n => n.Parent)
                .WithMany(n => n.ChildNodes)
                .WillCascadeOnDelete(false);
        }
    }
}
