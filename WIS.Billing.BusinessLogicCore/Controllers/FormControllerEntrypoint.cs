using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WIS.BusinessLogic.FormUtil.Coordinators;
using WIS.Common.Enums;
using WIS.Common.ServiceWrappers;

namespace WIS.BusinessLogic.Controllers
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
