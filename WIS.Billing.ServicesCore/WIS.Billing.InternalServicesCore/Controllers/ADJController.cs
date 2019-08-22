using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WIS.Billing.BusinessLogicCore.Controllers.Adjustments;
using WIS.BusinessLogicCore.Controllers;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.Session;

namespace WIS.Billing.InternalServicesCore.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    public class ADJController : Controller
    {
        #region ADJUSTMENTS
        [HttpPost]
        public async Task<IActionResult> ADJ010_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new AdjustmentController(session, null); //Ver si resolver con inyección de dependencias

                response = await entryPoint.InvokeAction(controller, data, null, cancelToken);

                response.SetSessionData(session.GetInnerDictionary());
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> ADJ010_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();

            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new AdjustmentController(session, null); //Ver si resolver con inyección de dependencias

                response = await entryPoint.InvokeAction(controller, data, null, cancelToken);

                response.SetSessionData(session.GetInnerDictionary());
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }


            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ADJ010_Form(FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new AdjustmentController(session, null); //Ver si resolver con inyección de dependencias

                response = await entryPoint.InvokeAction(controller, data, null, cancelToken);

                response.SetSessionData(session.GetInnerDictionary());
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return Ok(response);
        }

        #endregion
    }
}
