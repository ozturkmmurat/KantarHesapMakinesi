using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class KantarHesapMakinesiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

               //optionsBuilder.UseSqlServer("Server=YAYIN01;Database=WeighbridgeCalculator; User ID=murat;Password=123456;Connect Timeout=30;MultiSubnetFailover=False;");
               optionsBuilder.UseSqlServer("Server=94.102.74.13;Database=WeighbridgeCalculator; User ID=websa;Password=v2qySqsu7MkXL5D;Connect Timeout=30;MultiSubnetFailover=False;");
            }
        }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Electronic> Electronics { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeContent> SizeContents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductModelCost> ProductModelCosts { get; set; }
        public DbSet<CostVariable> CostVariables { get; set; }
        public DbSet<InstallationCost> InstallationCosts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<ProductProfit> ProductProfits { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModelCostDetail>(es =>
                es.HasNoKey()
            );

            modelBuilder.Entity<PasswordReset>()
              .HasKey(pr => pr.UserId); // Birebir iliski icin UserId'yi anahtar olarak belirle
        }
    }

}
