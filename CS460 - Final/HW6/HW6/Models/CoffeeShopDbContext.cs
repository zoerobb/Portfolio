using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HW6.Models;

public partial class CoffeeShopDbContext : DbContext
{
    public CoffeeShopDbContext()
    {
    }

    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderedItem> OrderedItems { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer("Name=CoffeeShopConnection");
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC27E196D51D");

            entity.ToTable("Delivery");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuItem__3214EC275AD67F75");

            entity.ToTable("MenuItem");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("Description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Price");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");

            entity.HasOne(d => d.Station).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("MenuItem_Fk_Station");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC27F1DC08CB");

            entity.ToTable("Order");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeliveryId).HasColumnName("Delivery_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
            entity.Property(e => e.StoreId).HasColumnName("Store_ID");
            entity.Property(e => e.TimeArrived)
                .HasColumnType("datetime")
                .HasColumnName("Time_Arrived");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Price");

            entity.HasOne(d => d.Delivery).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryId)
                .HasConstraintName("Order_Fk_Delivery");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("Order_Fk_Store");
        });

        modelBuilder.Entity<OrderedItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderedI__3214EC2722174594");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MenuItemId).HasColumnName("Menu_Item_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderedItems)
                .HasForeignKey(d => d.MenuItemId)
                .HasConstraintName("OrderedItems_Fk_MenuItem");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderedItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("OrderedItems_Fk_Order");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC270F79B42D");

            entity.ToTable("Station");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC27D50EE86F");

            entity.ToTable("Store");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
