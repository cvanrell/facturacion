using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WIS.Billing.WebSiteCore.Models.Managers
{
    public interface ISessionManager
    {
        void SetValue(string key, object value);
        object GetValue(string key);
        T GetValue<T>(string key);
    }
}
