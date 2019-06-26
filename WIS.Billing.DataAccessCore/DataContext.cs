using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using WIS.Billing.EntitiesCore;
using Microsoft.IdentityModel.Protocols;

namespace WIS.Billing.DataAccessCore
{
    //[DbConfigurationType(typeof(CodeConfig))]
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<HourRate> HourRates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Development> Developments { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }



        public DataContext(DbContextOptions options) : base(options)
        {
            //Database.SetInitializer(new DataContextDbInitializer());
            //var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLlocaldb;Database=WISBilling;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WISBillingDB"].ConnectionString);
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
