using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.DataModel.Queries;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.SortComponents;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.GridUtil.Services
{
    public interface IGridService
    {
        List<IGridColumn> GetColumns(WISDB context, string application, string gridId, int userId);
        List<IGridColumn> GetColumnsFromEntity<T>();
        List<GridRow> GetRows<T>(IQueryable<T> query, List<IGridColumn> columns, GridFetchRequest queryParameters, SortCommand defaultSorting, List<string> keys);
        List<GridRow> GetRows<T>(IQueryable<T> query, List<IGridColumn> columns, GridFetchRequest queryParameters, List<SortCommand> defaultSorting, List<string> keys);
        List<GridRow> GetRows<T>(IQueryObject<T> queryObject, List<IGridColumn> columns, GridFetchRequest queryParameters, SortCommand defaultSorting, List<string> keys);
        List<GridRow> GetRows<T>(IQueryObject<T> queryObject, List<IGridColumn> columns, GridFetchRequest queryParameters, List<SortCommand> defaultSorting, List<string> keys);
        void SaveAddedColumns(WISDB context, Grid grid, string application);
    }
}
