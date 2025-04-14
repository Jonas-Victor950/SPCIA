using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BRCSystem.ClassLibrary.Data
{
    public partial class DataContext
    {
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProcessFlow> ProcessFlows { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProgramEntity> ProgramsEntity { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        protected void ConfigureEnrollmentSystemEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>()
                .HasMany(m => m.Stations)
                .WithMany(s => s.Machines)
                .UsingEntity(j => j.ToTable("MachineStation"));

            modelBuilder.Entity<Material>()
                .HasOne(m => m.MaterialType)
                .WithMany(m => m.Materials)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Material>()
                .HasOne(m => m.Supplier)
                .WithMany(s => s.Materials)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProcessFlow>()
                .HasMany(p => p.ProcessSteps)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProcessFlow>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductSteps)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProcessFlow)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Drawing)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ImageTop)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ImageBottom)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ImageTopLabels)
                .WithOne()
                .HasForeignKey("ForeignKeyForTopLabels")
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ImageBottomLabels)
                .WithOne()
                .HasForeignKey("ForeignKeyForBottomLabels")
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(s => s.Capacities)
                .WithOne(c => c.Product)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasOne(u => u.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramEntity>()
                .HasOne(p => p.Station)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Station>()
                .HasMany(s => s.Operators)
                .WithMany(o => o.Stations)
                .UsingEntity(j => j.ToTable("StationUser"));
            modelBuilder.Entity<Station>()
                .HasMany(s => s.MaterialTypes)
                .WithMany(m => m.Stations)
                .UsingEntity(j => j.ToTable("StationMaterialType"));
            modelBuilder.Entity<Station>()
                .HasMany(s => s.Rejections)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}