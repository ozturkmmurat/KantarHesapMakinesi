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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModelCostDetail>(es =>
                es.HasNoKey()
            );
        }
    }

}
