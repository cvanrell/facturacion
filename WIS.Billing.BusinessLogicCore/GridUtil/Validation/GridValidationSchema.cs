using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.App;
using WIS.CommonCore.GridComponents;

namespace WIS.BusinessLogicCore.GridUtil.Validation
{
    public class GridValidationSchema : Dictionary<string, Func<IGridService, GridCell, GridRow, List<ComponentParameter>, int, WISDB, GridValidationGroup>>
    {
    }
}
