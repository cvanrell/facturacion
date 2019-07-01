using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.FilterComponents;

namespace WIS.BusinessLogicCore.FilterUtil
{
    public interface IFilter
    {
        IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string filterString);
        IQueryable<T> ApplyFilter<T>(IQueryable<T> query, List<FilterCommand> commands);
    }
}
