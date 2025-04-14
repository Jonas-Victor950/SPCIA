using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.EntityFrameworkCore;

namespace BRCSystem.ClassLibrary.Data
{
    public partial class DataContext
    {
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<ControlLimit> ControlLimits { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<Limit> Limits { get; set; }
        public DbSet<MachineProfile> MachineProfiles { get; set; }
        public DbSet<MachineProfileParameter> MachineProfileParameters { get; set; }
        public DbSet<MachineSetup> MachineSetups { get; set; }
        public DbSet<MachineSetupValue> MachineSetupValues { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessSampling> ProcessSamplings { get; set; }
        public DbSet<Samples> Samples { get; set; }
        public DbSet<TableParametersSPC> TableParametersSPC { get; set; }
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }


        protected void ConfigureSPCIAEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Characteristic>()
                .HasOne(c => c.UnitMeasurement)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ControlLimit>()
                    .HasOne(c => c.Process)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlLimit>()
                .HasOne(c => c.Machine)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ControlLimit>()
                .HasOne(c => c.User)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ControlLimit>()
                .HasMany(c => c.Limits)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Limit>()
                            .HasOne(l => l.Characteristic)
                            .WithMany()
                            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Limit>()
                .HasOne(l => l.SetBy)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachineProfile>()
            .HasMany(m => m.MachineProfileParameters)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MachineProfile>()
                .HasMany(m => m.Machines)
                .WithMany()
                .UsingEntity(j => j.ToTable("MachineProfileMachine"));

            modelBuilder.Entity<MachineProfileParameter>()
                .HasOne(m => m.UnitMeasurement)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachineSetup>()
           .HasMany(m => m.MachineSetupValues)
           .WithOne()
           .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MachineSetup>()
               .HasOne(m => m.Machine)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachineSetupValue>()
                .HasOne(m => m.MachineProfileParameter)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Process>()
                .HasMany(p => p.Characteristics)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Process>()
                .HasOne(p => p.Station)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Process>()
                .HasMany(p => p.Products)
                .WithMany()
                .UsingEntity(j => j.ToTable("ProcessProduct"));

            modelBuilder.Entity<ProcessSampling>()
                .HasOne(p => p.Process)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProcessSampling>()
                .HasOne(p => p.Machine)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProcessSampling>()
                .HasOne(p => p.Operator)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ProcessSampling>()
                .HasOne(p => p.ProductLot)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ProcessSampling>()
                .HasMany(p => p.Samples)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Samples>()
                .HasOne(s => s.Characteristic)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Samples>()
                .HasMany(s => s.Inputs)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}