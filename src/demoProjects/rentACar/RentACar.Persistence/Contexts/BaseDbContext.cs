﻿using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(a =>
            {
                a.ToTable("Brands").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Models);
            });

            modelBuilder.Entity<Model>(a =>
            {
                a.ToTable("Models").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.BrandId).HasColumnName("BrandId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                a.Property(p => p.ImageUrl).HasColumnName("ImageUrl");

                a.HasOne(p => p.Brand);

            });

            // ToDO: user, operationClaim, userOperationClaim ve refrehstokenlar için doldur



            Brand[] brandSEntitySeeds = { new(1, "BMW"), new(2, "Mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandSEntitySeeds);

            Model[] modelEntitySeeds = { new(1, 1, "X5", 100, "https://www.google.com"), new(2, 1, "X6", 200, "https://www.google.com"), new(3, 2, "C180", 150, "https://www.google.com") };
            modelBuilder.Entity<Model>().HasData(modelEntitySeeds);


        }
    }
}
