using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Project_3___Press_Project
{
    public class PressContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCatalog> OrderCatalogs { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopCatalog> ShopCatalogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserShop> UserShops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=Project3_PressV2;Integrated Security=True; MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserShop>()
                .HasKey(us => new { us.UserId, us. ShopId });
            modelBuilder.Entity<UserShop>()
                .HasOne(us => us.Shop)
                .WithMany(s => s.UserShops)
                .HasForeignKey(us => us.ShopId);
            modelBuilder.Entity<UserShop>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserShops)
                .HasForeignKey(us => us.UserId);


            modelBuilder.Entity<ShopCatalog>()
                .HasKey(sc => new { sc.CatalogId, sc.ShopId });
            modelBuilder.Entity<ShopCatalog>()
                .HasOne(sc => sc.Catalog)
                .WithMany(s => s.ShopCatalogs)
                .HasForeignKey(sc => sc.CatalogId);
            modelBuilder.Entity<ShopCatalog>()
                .HasOne(sc => sc.Shop)
                .WithMany(c => c.ShopCatalogs)
                .HasForeignKey(sc => sc.ShopId);

            modelBuilder.Entity<OrderCatalog>()
                .HasOne<Order>(oc => oc.Order)
                .WithMany(o => o.OrderCatalogs)
                .HasForeignKey(oc => oc.OrderId);

            modelBuilder.Entity<OrderCatalog>()
                .HasOne<Catalog>(c => c.Catalog)
                .WithOne(oc => oc.OrderCatalog);
        }
    }
}
