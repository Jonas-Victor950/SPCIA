using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;



namespace BRCSystem.ClassLibrary.Data

{
    public partial class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            ConfigureAuthenticationEntities(modelBuilder);

            ConfigureSPCIAEntities(modelBuilder);

            ConfigurePOSStationEntities(modelBuilder);

            ConfigureEnrollmentSystemEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}