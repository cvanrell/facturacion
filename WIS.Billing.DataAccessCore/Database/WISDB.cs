using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using WIS.Billing.EntitiesCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIS.Billing.EntitiesCore.Entities;
using WIS.Billing.EntitiesCore.QueryTypes;
using System.Data.SqlClient;
using System.IO;

namespace WIS.Billing.DataAccessCore.Database
{    
    public class WISDB : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<HourRate> HourRates { get; set; }
        public DbSet<SupportRate> SupportRates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Development> Developments { get; set; }        
        public DbSet<Adjustment> Adjustments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<T_GRID_USER_CONFIG> T_GRID_USER_CONFIG { get; set; }
        public DbSet<T_GRID_FILTER> T_GRID_FILTER { get; set; }
        public DbSet<T_GRID_FILTER_DET> T_GRID_FILTER_DET { get; set; }        
        public DbSet<T_GRID_DEFAULT_CONFIG> T_GRID_DEFAULT_CONFIG { get; set; }
        public DbSet<T_LOG_CLIENT> T_LOG_CLIENT { get; set; }
        public DbSet<T_LOG_FEE> T_LOG_FEE { get; set; }
        public DbSet<T_LOG_HOUR_RATE> T_LOG_HOUR_RATE { get; set; }
        public DbSet<T_LOG_SUPPORT_RATE> T_LOG_SUPPORT_RATE { get; set; }
        public DbSet<T_LOG_PROJECT> T_LOG_PROJECT { get; set; }
        public DbSet<T_LOG_SUPPORT> T_LOG_SUPPORT { get; set; }
        public DbSet<T_LOG_IPC> T_LOG_IPC { get; set; }
        public DbSet<T_LOG_RATE_ADJUSTMENTS> T_LOG_RATE_ADJUSTMENTS { get; set; }
        public DbSet<T_LOG_BILL> T_LOG_BILL { get; set; }

        //VISTAS
        public DbQuery<H_HOUR_RATE> H_HOUR_RATE { get; set; }
        public DbQuery<H_SUPPORT_RATE> H_SUPPORT_RATE { get; set; }
        public DbQuery<V_HOUR_RATES> V_HOUR_RATES { get; set; }
        public DbQuery<V_SUPPORT_RATES> V_SUPPORT_RATES { get; set; }
        public DbQuery<V_LAST_T_LOG_RATE_ADJUSTMENTS> V_LAST_T_LOG_RATE_ADJUSTMENTS { get; set; }

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
            //con = new SqlConnection();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLlocaldb;Database=WISBilling;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Server=192.168.100.89;Database=WISBilling; user=wis; password= W1sTCU17");
            //optionsBuilder.UseSqlServer(@"Server=192.168.100.105;Database=WISBilling; Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WISBillingDB"].ConnectionString);
            //SITCUWISAPPHOST
        }

        //public IConfiguration GetConfiguration()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).Add("App.config", optional: true, reloadOnChange: true);
        //    return builder.Build();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_GRID_DEFAULT_CONFIG>()
                .HasKey(c => new { c.CD_APLICACION, c.CD_BLOQUE, c.NM_DATAFIELD });
            modelBuilder.Entity<T_GRID_USER_CONFIG>()
                .HasKey(d => new { d.CD_APLICACION, d.CD_BLOQUE, d.NM_DATAFIELD });
            modelBuilder.Entity<T_GRID_FILTER_DET>()
                .HasKey(d => new { d.CD_FILTRO, d.CD_COLUMNA });
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
