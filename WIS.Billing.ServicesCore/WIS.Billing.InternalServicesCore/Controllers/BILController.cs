using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WIS.Billing.BusinessLogicCore.Controllers.Billing;
using WIS.BusinessLogicCore.Controllers;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.Session;

namespace WIS.Billing.InternalServicesCore.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    public class BILController : Controller
    {
        #region BILLING
        [HttpPost]
        public async Task<IActionResult> BIL010_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new BillingController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> BIL010_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();

            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new BillingController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> BIL010_Form([FromBody]FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new BillingController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> BIL020_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new BillingController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> BIL020_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();

            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new BillingController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> BIL020_Form([FromBody]FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new BillingController(session, null); //Ver si resolver con inyección de dependencias

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