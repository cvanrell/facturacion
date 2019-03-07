using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Billing.DataAccess
{
    public class DataContextFactory : IDbContextFactory<DataContext>
    {
        public DataContext Create()
        {
            return new DataContext("Server=.\\SQLEXPRESS;Database=WISBilling;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
