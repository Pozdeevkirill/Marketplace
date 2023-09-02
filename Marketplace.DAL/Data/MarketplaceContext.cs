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
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public MarketplaceContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role admin = new() { Id = 1, Name = "admin" }; // Админ сайта
            Role moderator = new() { Id = 2, Name = "moderator" }; // Модератор сайта
            Role user = new() { Id = 3, Name = "user" }; // Обычный покупатель
            Role adminCompany = new() { Id = 4, Name = "adminCompany" }; // Администратор магазина
                                                                            /*
                                                                             Администратор магазина может назначать 
                                                                            модераторов магазина. У них будет своя панель, 
                                                                            где будут отображаться товары их компании

                                                                            Они смогут отвечать на вопросы клиентов и отзывы
                                                                            Но магазином будет сам сайт.
                                                                            Как в ДНС
                                                                             */
            Role moderatorCompany = new() { Id = 5, Name = "moderatorCompany" };

            modelBuilder.Entity<Role>().HasData(new Role[] { admin, moderator, user, adminCompany, moderatorCompany });

            base.OnModelCreating(modelBuilder);
        }
    }
}
