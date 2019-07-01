using WIS.CommonCore.GridComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.Session;

namespace WIS.BusinessLogicCore.GridUtil.Coordinators
{
    public interface IGridCoordinator
    {
        IGridWrapper Initialize(IGridWrapper wrapper);
        IGridWrapper FetchRows(IGridWrapper wrapper);
        IGridWrapper ValidateRow(IGridWrapper wrapper);
        IGridWrapper Commit(IGridWrapper wrapper);
        IGridWrapper ButtonAction(IGridWrapper wrapper);
        IGridWrapper MenuItemAction(IGridWrapper wrapper);
    }
}
