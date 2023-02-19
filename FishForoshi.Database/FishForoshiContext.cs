﻿using FishForoshi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishForoshi.Database
{
    public class FishForoshiContext : DbContext
    {
        public static string ConnectionString { get; set; } = null!;
        public FishForoshiContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplySoftDelete();
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }

        public DbSet<Food> Foods { get; set; } = null!;
        public DbSet<Norm> Norms { get; set; } = null!;

    }
}