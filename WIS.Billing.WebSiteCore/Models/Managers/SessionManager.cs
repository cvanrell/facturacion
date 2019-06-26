using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIS.Billing.WebSiteCore.Extensions;

namespace WIS.Billing.WebSiteCore.Models.Managers
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        const string USER_SESSION_BAG = "UserSessionBag";
        const string WIS_SESSION = "WIS_SESSION";

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public void SetValue(string key, object value)
        {
            Dictionary<string, object> sessionBag = this._httpContextAccessor.HttpContext.Session.Get<Dictionary<string, object>>(USER_SESSION_BAG);

            if (sessionBag == null)
                sessionBag = new Dictionary<string, object>();

            if (value != null)
                sessionBag[key] = value;
            else
                sessionBag.Remove(key);

            this._httpContextAccessor.HttpContext.Session.Set(USER_SESSION_BAG, sessionBag);

        }
        public object GetValue(string key)
        {
            Dictionary<string, object> sessionBag = this._httpContextAccessor.HttpContext.Session.Get<Dictionary<string, object>>(USER_SESSION_BAG);

            if(sessionBag == null)
                return null;

            if (!sessionBag.ContainsKey(key))
                return null;

            return sessionBag[key];
        }
        public T GetValue<T>(string key)
        {
            Dictionary<string, object> sessionBag = this._httpContextAccessor.HttpContext.Session.Get<Dictionary<string, object>>(USER_SESSION_BAG);

            if (sessionBag == null)
                return default(T);

            if (!sessionBag.ContainsKey(key))
                return default(T);

            return (T)sessionBag[key];
        }
    }
}
