using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.Session
{
    public interface ISessionAccessor
    {
        T GetValue<T>(string key);
        Dictionary<string, object> GetInnerDictionary();

        void SetValue(string key, object value);

        bool ContainsKey(string key);
    }
}
