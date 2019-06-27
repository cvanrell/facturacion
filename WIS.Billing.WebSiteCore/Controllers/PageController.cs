using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WIS.CommonCore.Enums;
using WIS.Billing.WebSiteCore.Models;
using WIS.CommonCore.WebApi;
using WIS.Billing.WebSiteCore.Models.Managers;
using WIS.CommonCore.App;
using WIS.CommonCore.Page;
using WIS.CommonCore.ServiceWrappers;
using System.Net.Http;

namespace WIS.Billing.WebSiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : BaseController
    {
        private readonly IWebApiClient _apiClient;
        private readonly ISessionManager _sessionManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public PageController(IWebApiClient apiClient, ISessionManager sessionManager, IHttpClientFactory httpClientFactory) : base()
        {
            _apiClient = apiClient;
            _sessionManager = sessionManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Load([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IPageWrapper responseData = await this.CallPageServiceAsync(serverRequest, PageAction.Load, cancelToken);

            var content = responseData.GetResolvedData<PageQueryData>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Unload([FromBody]ServerRequest serverRequest, CancellationToken cancelToken)
        {
            //TODO: Comprobar permisos de usuario antes de realizar llamada, para comprobar que puede acceder a la aplicación provista
            IPageWrapper responseData = await this.CallPageServiceAsync(serverRequest, PageAction.Unload, cancelToken);

            var content = responseData.GetResolvedData<PageQueryData>();

            var response = new ServerResponse(content);

            if (responseData.Status == CommonCore.Enums.TransferWrapperStatus.Error)
                response.SetError(responseData.Message);

            return Content(response.Serialize(), "application/json");
        }

        private async Task<IPageWrapper> CallPageServiceAsync(ServerRequest request, PageAction action, CancellationToken cancelToken)
        {
            string controller = request.GetBaseApplication();
            string application = request.Application;

            var session = _sessionManager.GetValue<string>("WIS_SESSION");

            if (session == null)
                session = JsonConvert.SerializeObject(new Dictionary<string, object>());

            var transferData = new PageWrapper
            {
                Application = application,
                Action = action,
                User = 0,
                Data = request.Data,
                SessionData = session,
                PageToken = ""
            };

            var client = this._httpClientFactory.CreateClient();

            var result = await _apiClient.PostAsync(client, "http://localhost:51802/", controller, application + "_Page", transferData, cancelToken);

            if(!string.IsNullOrEmpty(result.SessionData))
                _sessionManager.SetValue("WIS_SESSION", result.SessionData);

            return result;
        }
    }
}