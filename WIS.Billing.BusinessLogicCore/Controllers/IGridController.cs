﻿using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WIS.BusinessLogicCore.GridUtil.Validation;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.App;

namespace WIS.BusinessLogicCore.Controllers
{
    public interface IGridController
    {
        Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest query, int userId);
        //Grid GridInitialize(IGridService service, Grid grid, GridInitializeQuery query);
        Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest query, int userId);
        Grid GridValidateRow(IGridService service, GridRow row, Grid grid, List<ComponentParameter> parameters, int userId);
        Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId);
        GridMenuItemActionQuery GridMenuItemAction(IGridService service, GridMenuItemActionQuery selection, int userId);
        GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId);
    }
}
