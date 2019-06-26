using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WIS.CommonCore.WebApi
{
    public interface IWebApiClient
    {
        Task<T> PostAsync<T>(HttpClient client, string uri, string application, string operation, T transferObject, CancellationToken cancelToken);
        Task<T> PostAsync<T>(HttpClient client, string uri, string application, T transferObject, CancellationToken cancelToken);
    }
}
