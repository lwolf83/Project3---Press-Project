using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Project_3___Press_Project
{
    public class PressContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderNewspaper> OrderNewspapers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopNewspaper> ShopNewspapers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=Project3_Press;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Join table for OrderNewspaper
            modelBuilder.Entity<OrderNewspaper>()
                .HasKey(on => new { on.OrderId, on.NewspaperId });
            modelBuilder.Entity<OrderNewspaper>()
                .HasOne(on => on.Order)
                .WithMany(o => o.OrderNewspapers)
                .HasForeignKey(on => on.Order);
            modelBuilder.Entity<OrderNewspaper>()
                .HasOne(on => on.Newspaper)
                .WithMany(n => n.OrderNewspaper)
                .HasForeignKey(on => on.NewspaperId);

            // Join table for ShopNewspaper
            modelBuilder.Entity<ShopNewspaper>()
                .HasKey(sn => new { sn.ShopId, sn.NewspaperId });
            modelBuilder.Entity<ShopNewspaper>()
                .HasOne(sn => sn.Shop)
                .WithMany(s => s.ShopNewspaper)
                .HasForeignKey(sn => sn.Shop);
            modelBuilder.Entity<ShopNewspaper>()
                .HasOne(sn => sn.Newspaper)
                .WithMany(n => n.ShopNewspaper)
                .HasForeignKey(on => on.NewspaperId);
        }
    }
}
