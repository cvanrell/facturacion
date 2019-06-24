using System;
using System.Data.Entity;
using WIS.Billing.Entities;

namespace WIS.Billing.DataAccess
{
    //[DbConfigurationType(typeof(CodeConfig))]
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<HourRate> HourRates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Development> Developments { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }


        public DataContext() : base("name=WISBillingDB")
        {
            Database.SetInitializer(new DataContextDbInitializer());
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
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
