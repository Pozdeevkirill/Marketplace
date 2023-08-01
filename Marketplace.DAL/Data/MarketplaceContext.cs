using Marketplace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Marketplace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Data
{
    public class MarketplaceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public MarketplaceContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role admin = new() { Id = 1, Name = "admin" };
            Role moderator = new() { Id = 2, Name = "moderator" };
            Role user = new() { Id = 3, Name = "user" };
            Role adminCompany = new() { Id = 4, Name = "adminCompany" };
            Role moderatorCompany = new() { Id = 5, Name = "moderatorCompany" };

            modelBuilder.Entity<Role>().HasData(new Role[] { admin, moderator, user, adminCompany, moderatorCompany });

            base.OnModelCreating(modelBuilder);
        }
    }
}
