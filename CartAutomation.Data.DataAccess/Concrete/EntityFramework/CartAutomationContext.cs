using CartAutomation.Data.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartAutomation.Data.DataAccess.Concrete.EntityFramework
{
    public class CartAutomationContext : DbContext
    { 
        public DbSet<Product> Product{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Server=(localdb)\mssqllocaldb;Database=CartAutomation;Trusted_Connection=true");
        }
       
    }
}
