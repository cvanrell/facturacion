using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FormUtil.Coordinators;
using WIS.CommonCore.Enums;
using WIS.CommonCore.ServiceWrappers;

namespace WIS.BusinessLogicCore.Controllers
{
    public class FormControllerEntrypoint
    {
        public async Task<IFormWrapper> InvokeAction(IFormController controller, IFormWrapper wrapper, IDbConnection connection, CancellationToken cancelToken)
        {
            return await Task.Run(() =>
            {
                IFormWrapper response = new FormWrapper();

                try
                {
                    IFormCoordinator coordinator = new FormCoordinator(controller, connection);

                    switch (wrapper.Action)
                    {
                        case FormAction.Initialize:
                            response = coordinator.Initialize(wrapper);
                            break;
                        case FormAction.ValidateField:
                            response = coordinator.ValidateField(wrapper);
                            break;
                        case FormAction.ButtonAction:
                            response = coordinator.ButtonAction(wrapper);
                            break;
                        case FormAction.Submit:
                            response = coordinator.Submit(wrapper);
                            break;
                        case FormAction.SelectSearch:
                            response = coordinator.SelectSearch(wrapper);
                            break;
                        case FormAction.ExecuteAdjustment:
                            response = coordinator.ExecuteAdjustments(wrapper);
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
