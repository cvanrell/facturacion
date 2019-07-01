using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.DataModel.Queries
{
    public interface IQueryObject<T>
    {
        IQueryable<T> GetQuery();
    }
}
