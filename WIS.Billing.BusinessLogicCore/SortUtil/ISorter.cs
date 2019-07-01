using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.SortComponents;

namespace WIS.BusinessLogicCore.SortUtil
{
    public interface ISorter
    {
        IQueryable<T> ApplySorting<T>(IQueryable<T> query, List<SortCommand> commands);
        IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortCommand command);
    }
}
