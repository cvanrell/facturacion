using WIS.Billing.BusinessLogicCore.Controllers;
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

namespace WIS.Billing.InternalServicesCore.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    //[ApiController]
    public class CLIController : BaseController
    {
        [System.Web.Http.HttpPost]
        public async Task<System.Web.Http.IHttpActionResult> Clients_Page(PageWrapper data, CancellationToken cancelToken)
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
        public async Task<System.Web.Http.IHttpActionResult> CLIENTS_Grid([FromBody]GridWrapper data, CancellationToken cancelToken)
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

        
        public void Post([FromBody]Object value)
        {
            //Prueba p = value;
            //Prueba p = new Prueba(value);
        }
        [System.Web.Http.HttpPost]
        public async Task<System.Web.Http.IHttpActionResult> Clients_Form(FormWrapper data, CancellationToken cancelToken)
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
    }    
}