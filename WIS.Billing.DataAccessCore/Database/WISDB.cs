using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using WIS.Billing.EntitiesCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WIS.Billing.DataAccessCore.Database
{    
    public class WISDB : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<HourRate> HourRates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Development> Developments { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<T_GRID_USER_CONFIG> T_GRID_USER_CONFIG { get; set; }
        public DbSet<T_GRID_DEFAULT_CONFIG> T_GRID_DEFAULT_CONFIG { get; set; }
        //public static DbContextOptions options;


        public WISDB() /*: base(options)*/
        {
            //Database.SetInitializer(new DataContextDbInitializer());
            //var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public WISDB(DbContextOptions options) : base(options)
        {
            //Database.SetInitializer(new DataContextDbInitializer());
            //var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLlocaldb;Database=WISBilling;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WISBillingDB"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_GRID_DEFAULT_CONFIG>()
                .HasKey(c => new { c.CD_APLICACION, c.CD_BLOQUE, c.NM_DATAFIELD });
            modelBuilder.Entity<T_GRID_USER_CONFIG>()
                .HasKey(d => new { d.CD_APLICACION, d.CD_BLOQUE, d.NM_DATAFIELD });
        }

    }

    //public class CodeConfig : DbConfiguration
    //{
    //    public CodeConfig()
    //    {
    //        SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
    //    }
    //}
}
