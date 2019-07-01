using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WIS.BusinessLogic.Page.Coordinators;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.Page.Coordinators;
using WIS.CommonCore.Enums;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.Session;

namespace WIS.BusinessLogicCore.Controllers
{
    public class PageEntrypoint
    {
        public async Task<IPageWrapper> InvokeAction(IController controller, IPageWrapper wrapper, IDbConnection connection, CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                IPageWrapper response = new PageWrapper();

                try
                {
                    IPageCoordinator handler = new PageCoordinator(controller, connection);

                    switch (wrapper.Action)
                    {
                        case PageAction.Load:
                            response = handler.Load(wrapper);
                            break;
                        case PageAction.Unload:
                            response = handler.Unload(wrapper);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    response.SetError(ex.Message);
                }

                return response;
            }, cancelToken);
        }
    }
}
