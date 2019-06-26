using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WIS.Billing.WebSiteCore.Models
{
    public class ServerResponse
    {
        public string Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public ServerResponse()
        {

        }

        public ServerResponse(string data)
        {
            this.Data = data;
            this.Status = "OK";
        }

        public ServerResponse(string data, string status, string message)
        {
            this.Data = data;
            this.Status = status;
            this.Message = message;
        }

        public void SetError(string message)
        {
            this.Status = "ERROR";
            this.Message = message;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
