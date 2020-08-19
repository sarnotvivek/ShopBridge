using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.DAL.Model
{
    public class ShopBridgeDBContext : DbContext
    {
        public ShopBridgeDBContext()
        {

        }
        public ShopBridgeDBContext(DbContextOptions<ShopBridgeDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Item>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id).IsRequired();
            });

        }
    }
}
