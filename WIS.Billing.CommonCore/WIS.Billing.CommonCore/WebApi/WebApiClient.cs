using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WIS.CommonCore.WebApi
{
    public class WebApiClient : IWebApiClient
    {
        public async Task<T> PostAsync<T>(HttpClient client, string uri, string application, string operation, T transferObject, CancellationToken cancelToken)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var address = new Uri(new Uri(uri), "api/" + application + "/" + operation);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address);

            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request, cancelToken);
            //var response = await client.SendAsync(request, cancelToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error, status: " + response.StatusCode + " - " + response.ReasonPhrase + "-" + await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
        public async Task<T> PostAsync<T>(HttpClient client, string uri, string application, T transferObject, CancellationToken cancelToken)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var address = new Uri(new Uri(uri), "api/" + application);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address);

            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request, cancelToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error, status: " + response.StatusCode + " - " + response.ReasonPhrase + "-" + await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
