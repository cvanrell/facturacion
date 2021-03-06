﻿using WIS.Billing.BusinessLogicCore.Controllers;
using WIS.Billing.BusinessLogicCore.Controllers.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
//using System.Web.Http;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.Session;
using WIS.BusinessLogicCore.Controllers;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using WIS.Billing.BusinessLogicCore.Controllers.Rates;

namespace WIS.Billing.InternalServicesCore.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    //[ApiController]
    //[Produces("application/json")]
    public class CLIController : Controller
    {

        #region CLIENTS
        [HttpPost]
        public async Task<IActionResult> CLI010_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new ClientController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI010_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();

            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new ClientController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI010_Form(FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new ClientController(session, null); //Ver si resolver con inyección de dependencias

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

        #region DET_CLIENTS

        [HttpPost]
        public async Task<IActionResult> CLI020_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new DetClientController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI020_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();
            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new DetClientController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI020_Form([FromBody]FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new DetClientController(session, null); //Ver si resolver con inyección de dependencias

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

        #region HISTORICO

        [HttpPost]
        public async Task<IActionResult> CLI030_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new HourRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI030_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();
            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new HourRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI030_Form([FromBody]FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new HourRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI040_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new SupportRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI040_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();
            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new SupportRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI040_Form([FromBody]FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new SupportRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI050_Page(PageWrapper data, CancellationToken cancelToken)
        {
            IPageWrapper response = new PageWrapper();

            try
            {
                var entryPoint = new PageEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IController controller = new HourRateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI050_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
        {
            IGridWrapper response = new GridWrapper();
            try
            {
                var entryPoint = new GridControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IGridController controller = new RateController(session, null); //Ver si resolver con inyección de dependencias

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
        public async Task<IActionResult> CLI050_Form([FromBody]FormWrapper data, CancellationToken cancelToken)
        {
            IFormWrapper response = new FormWrapper();

            try
            {
                var entryPoint = new FormControllerEntrypoint();

                ISessionAccessor session = new SessionAccessor(data.GetSessionData());

                IFormController controller = new HourRateController(session, null); //Ver si resolver con inyección de dependencias

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