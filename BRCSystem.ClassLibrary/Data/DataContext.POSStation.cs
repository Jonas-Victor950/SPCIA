using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.Logs;
using BRCSystem.ClassLibrary.POSStation.Entities;
using Microsoft.EntityFrameworkCore;

namespace BRCSystem.ClassLibrary.Data
{
    public partial class DataContext
    {
        public DbSet<ProcessStep> ProcessSteps { get; set; }
        public DbSet<ProductStep> ProductSteps { get; set; }
        public DbSet<ImageLabel> ImageLabels { get; set; }
        public DbSet<LotStepData> LotStepDatas { get; set; }
        public DbSet<ProductLot> ProductLots { get; set; }
        public DbSet<LTCConfig> LTCConfigs { get; set; }
        public DbSet<Rejection> Rejections { get; set; }
        public DbSet<RejectInformation> RejectInformations { get; set; }
        public DbSet<LotStepDataMaterial> LotStepDataMaterials { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<StripRejection> StripRejections { get; set; }
        public DbSet<SWR> SWRs { get; set; }
        public DbSet<YieldControl> YieldControls { get; set; }
        public DbSet<LotStatusHistory> LotStatusHistories { get; set; }
        public DbSet<ExposureTimeConfiguration> ExposureTimeConfigurations { get; set; }
        public DbSet<SamsungCode> SamsungCodes { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected void ConfigurePOSStationEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessStep>()
                .HasOne(p => p.Station)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductStep>()
                .HasOne(p => p.ProcessStep)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductStep>()
                .HasOne(p => p.ProgramEntity)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductStep>()
                .HasMany(p => p.Materials)
                .WithMany(m => m.ProductSteps)
                .UsingEntity(j => j.ToTable("ProductStepProductStep"));
            modelBuilder.Entity<ProductStep>()
                .HasOne(p => p.StartYield)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotStepData>()
                .HasOne(l => l.OperadorIn)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LotStepData>()
                .HasOne(l => l.OperadorOut)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LotStepData>()
                .HasOne(l => l.ProductStep)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LotStepData>()
                .HasMany(l => l.RejectInformations)
                .WithOne(r => r.LotStepData)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LotStepData>()
                .HasOne(l => l.Machine)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LotStepData>()
                .HasOne(l => l.StartYield)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductLot>()
                .HasOne(p => p.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductLot>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductLot>()
                .HasMany(p => p.LotStepDatas)
                .WithOne(l => l.ProductLot)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductLot>()
                .HasOne(p => p.LTCConfig)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductLot>()
                .HasOne(p => p.ProductLotOrigin)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductLot>()
                .HasOne(p => p.LotStepDataInit)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RejectInformation>()
                .HasOne(r => r.Rejection)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotStepDataMaterial>()
                .HasOne(l => l.Material)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LotStepDataMaterial>()
                .HasOne(l => l.LotStepData)
                .WithMany(l => l.LotStepDataMaterials);

            modelBuilder.Entity<Capacity>()
                .HasOne(c => c.ProductReferency)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StripRejection>()
                .HasOne(s => s.LotStepData)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<SWR>()
                .HasOne(s => s.ProductLot)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<SWR>()
                .HasOne(s => s.LotStepData)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<LotStatusHistory>()
                .HasOne(p => p.ProductLot)
                .WithMany(l => l.LotStatusHistories)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LotStatusHistory>()
                .HasOne(u => u.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExposureTimeConfiguration>()
                .HasOne(e => e.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PasswordHistory>()
                .HasOne(p => p.User)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}