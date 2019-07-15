using WIS.CommonCore.GridComponents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WIS.Billing.WebSiteCore.Models;
using WIS.CommonCore.Enums;
using WIS.CommonCore.WebApi;
using WIS.Billing.WebSiteCore.Models.Managers;
using WIS.CommonCore.ServiceWrappers;
using System.Net.Http;

namespace WIS.Billing.WebSiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridController : BaseController
    {
        private readonly IWebApiClient _apiClient;
        private readonly ISessionManager _sessionManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public GridController(IWebApiClient apiClient, ISessionManager sessionManager, IHttpClientFactory httpClientFactory) : base()
        {
            _apiClient = apiClient;
            _sessionManager = sessionManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Initialize([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IGridWrapper responseData = await this.CallGridServiceAsync(serverRequest, GridAction.Initialize, cancelToken);

            var content = responseData.GetResolvedData<GridInitializeResponse>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> FetchRows([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            var responseData = await this.CallGridServiceAsync(serverRequest, GridAction.FetchRows, cancelToken);

            var content = responseData.GetResolvedData<GridFetchResponse>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ValidateRow([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            var responseData = await this.CallGridServiceAsync(serverRequest, GridAction.ValidateRow, cancelToken);

            var content = responseData.GetResolvedData<GridValidationResponse>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Commit([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            var responseData = await this.CallGridServiceAsync(serverRequest, GridAction.Commit, cancelToken);

            var content = responseData.GetResolvedData<GridFetchResponse>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ButtonAction([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            var responseData = await this.CallGridServiceAsync(serverRequest, GridAction.ButtonAction, cancelToken);

            var content = responseData.GetResolvedData<GridButtonAction>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> MenuItemAction([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            var responseData = await this.CallGridServiceAsync(serverRequest, GridAction.MenuItemAction, cancelToken);

            var content = responseData.GetResolvedData<GridMenuItemAction>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateConfig([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            var responseData = await this.CallGridServiceAsync(serverRequest, "/Grid/UpdateConfig", GridAction.UpdateConfig, cancelToken);
            
            var response = new ServerResponse();

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        private async Task<IGridWrapper> CallGridServiceAsync(ServerRequest request, GridAction action, CancellationToken cancelToken)
        {
            GridWrapper result;

            try
            {
                string controller = request.GetBaseApplication();
                string application = request.Application;

                var session = _sessionManager.GetValue<string>("WIS_SESSION");

                if (session == null)
                    session = JsonConvert.SerializeObject(new Dictionary<string, object>());

                var transferData = new GridWrapper
                {
                    Application = application,
                    Action = action, //TODO: Ver si quitar
                    GridId = request.ComponentId,
                    User = 0,
                    Data = request.Data,
                    SessionData = session,
                    PageToken = ""
                };

                var client = _httpClientFactory.CreateClient();

                result = await _apiClient.PostAsync(client, "https://localhost:44340/", controller, application + "_Grid", transferData, cancelToken);

                if (!string.IsNullOrEmpty(result.SessionData))
                    _sessionManager.SetValue("WIS_SESSION", result.SessionData);

            }
            catch(Exception ex)
            {
                throw;
            }

            return result;
        }
        private async Task<IGridWrapper> CallGridServiceAsync(ServerRequest request, string url, GridAction action, CancellationToken cancelToken)
        {
            var session = _sessionManager.GetValue<string>("WIS_SESSION");

            if (session == null)
                session = JsonConvert.SerializeObject(new Dictionary<string, object>());

            var transferData = new GridWrapper
            {
                Application = request.Application,
                Action = action, //TODO: Ver si quitar
                GridId = request.ComponentId,
                User = 0,
                Data = request.Data,
                SessionData = session,
                PageToken = ""
            };

            var client = _httpClientFactory.CreateClient();

            var result = await _apiClient.PostAsync(client, "http://localhost:51802/", url, transferData, cancelToken);

            if (!string.IsNullOrEmpty(result.SessionData))
                _sessionManager.SetValue("WIS_SESSION", result.SessionData);

            return result;
        }
    }
}
