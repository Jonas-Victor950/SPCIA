using BRCSystem.ClassLibrary.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace BRCSystem.ClassLibrary.Data
{
    public partial class DataContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Parameters> Parameters { get; set; }


        protected void ConfigureAuthenticationEntities(ModelBuilder modelBuilder)
        {
            
        }    
    }
}