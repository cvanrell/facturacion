using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.DataModel.Queries
{
    public interface IQueryObject<T>
    {
        IQueryable<T> BuildQuery(WISDB context);

        IQueryable<T> GetQuery();
    }
}
