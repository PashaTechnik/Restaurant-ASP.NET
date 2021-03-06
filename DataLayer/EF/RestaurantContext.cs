﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataLayer
{
    public partial class RestaurantContext : DbContext
    {
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dish { get; set; }
        public virtual DbSet<Dishdetails> Dishdetails { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Orderdetails> Orderdetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("myDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("dish");

                entity.Property(e => e.Dishid).HasColumnName("dishid");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Dishdetails>(entity =>
            {
                entity.HasKey(e => new { e.Dishid, e.Ingredientid })
                    .HasName("pk_dish_details");

                entity.ToTable("dishdetails");

                entity.Property(e => e.Dishid).HasColumnName("dishid");

                entity.Property(e => e.Ingredientid).HasColumnName("ingredientid");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Dishdetails)
                    .HasForeignKey(d => d.Dishid)
                    .HasConstraintName("fk_dish_details_dish");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.Dishdetails)
                    .HasForeignKey(d => d.Ingredientid)
                    .HasConstraintName("fk_dish_details_ingredient");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.Property(e => e.Ingredientid).HasColumnName("ingredientid");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.Positionid)
                    .HasName("menu_pkey");

                entity.ToTable("menu");

                entity.Property(e => e.Positionid).HasColumnName("positionid");

                entity.Property(e => e.Dishid).HasColumnName("dishid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.Dishid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_menu_dish");
            });

            modelBuilder.Entity<Orderdetails>(entity =>
            {
                entity.HasKey(e => new { e.Positionid, e.Orderid })
                    .HasName("pk_order_details");

                entity.ToTable("orderdetails");

                entity.Property(e => e.Positionid).HasColumnName("positionid");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");



                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.Positionid)
                    .HasConstraintName("fk_order_details_menu");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("orders_pkey");

                entity.ToTable("orders");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Clientname)
                    .HasColumnName("clientname")
                    .HasMaxLength(30);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
