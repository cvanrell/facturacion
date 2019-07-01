using WIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WIS.BusinessLogic.GridUtil.Coordinators;
using WIS.Common.ServiceWrappers;
using WIS.Common.Session;
using WIS.BusinessLogic.GridUtil.Services;
using System.Data;
using WIS.BusinessLogic.SortUtil;
using WIS.BusinessLogic.FilterUtil;

namespace WIS.BusinessLogic.Controllers
{
    public class GridControllerEntrypoint
    {
        public async Task<IGridWrapper> InvokeAction(IGridController controller, IGridWrapper wrapper, IDbConnection connection, CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                IGridWrapper response = new GridWrapper();

                try
                {
                    IFilter filter = new Filter();
                    ISorter sorter = new Sorter();

                    IGridService service = new GridService(filter, sorter);

                    IGridCoordinator coordinator = new GridCoordinator(controller, service, connection);

                    switch (wrapper.Action)
                    {
                        case GridAction.Initialize:
                            response = coordinator.Initialize(wrapper);
                            break;
                        case GridAction.FetchRows:
                            response = coordinator.FetchRows(wrapper);
                            break;
                        case GridAction.ValidateRow:
                            response = coordinator.ValidateRow(wrapper);
                            break;
                        case GridAction.Commit:
                            response = coordinator.Commit(wrapper);
                            break;
                        case GridAction.ButtonAction:
                            response = coordinator.ButtonAction(wrapper);
                            break;
                        case GridAction.MenuItemAction:
                            response = coordinator.MenuItemAction(wrapper);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    response.SetError(ex.Message);
                }

                return response;
            });
        }
    }
}
