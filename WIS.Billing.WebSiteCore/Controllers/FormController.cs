using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.WebApi;
using WIS.Billing.WebSiteCore.Models;
using WIS.Billing.WebSiteCore.Models.Managers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WIS.Billing.WebSiteCore.Controllers
{
    public class FormController : BaseController
    {
        private readonly IWebApiClient _apiClient;
        private readonly ISessionManager _sessionManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public FormController(IWebApiClient apiClient, ISessionManager sessionManager, IHttpClientFactory httpClientFactory) : base()
        {
            _apiClient = apiClient;
            _sessionManager = sessionManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Initialize([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IFormWrapper responseData = await this.CallFormServiceAsync(serverRequest, FormAction.Initialize, cancelToken);

            var content = responseData.GetResolvedData<FormData>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> ValidateField([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IFormWrapper responseData = await this.CallFormServiceAsync(serverRequest, FormAction.ValidateField, cancelToken);

            var content = responseData.GetResolvedData<FormValidationData>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> Submit([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IFormWrapper responseData = await this.CallFormServiceAsync(serverRequest, FormAction.Submit, cancelToken);

            var content = responseData.GetResolvedData<FormSubmitData>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> ButtonAction([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IFormWrapper responseData = await this.CallFormServiceAsync(serverRequest, FormAction.ButtonAction, cancelToken);

            var content = responseData.GetResolvedData<FormButtonActionData>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        private async Task<IFormWrapper> CallFormServiceAsync(ServerRequest request, FormAction action, CancellationToken cancelToken)
        {
            string controller = request.GetBaseApplication();
            string application = request.Application;

            var session = _sessionManager.GetValue<string>("WIS_SESSION");

            if (session == null)
                session = JsonConvert.SerializeObject(new Dictionary<string, object>());

            var transferData = new FormWrapper
            {
                Application = application,
                //User = user.UserId,
                Action = action, //TODO: Ver si quitar
                FormId = request.ComponentId,
                User = 0,
                Data = request.Data,
                SessionData = session,
                PageToken = ""
            };

            var client = this._httpClientFactory.CreateClient();

            var result = await _apiClient.PostAsync(client, "https://localhost:44340/", controller, application + "_Form", transferData, cancelToken);

            if (!string.IsNullOrEmpty(result.SessionData))
                _sessionManager.SetValue("WIS_SESSION", result.SessionData);

            return result;
        }
    }
}
