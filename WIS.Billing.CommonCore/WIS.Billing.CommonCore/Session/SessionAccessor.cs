using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.Session
{
    public class SessionAccessor : ISessionAccessor
    {
        private Dictionary<string, object> InnerDictionary;

        public SessionAccessor()
        {
            this.InnerDictionary = new Dictionary<string, object>();
        }
        public SessionAccessor(Dictionary<string, object> dictionary)
        {
            this.InnerDictionary = dictionary;
        }

        public T GetValue<T>(string key)
        {
            if (this.InnerDictionary.ContainsKey(key))
            {
                if (this.InnerDictionary[key] is JObject)
                    this.InnerDictionary[key] = ((JObject)this.InnerDictionary[key]).ToObject<T>();

                return (T)this.InnerDictionary[key];
            }
            else
            {
                return default(T);
            }
        }
        public Dictionary<string, object> GetInnerDictionary()
        {
            return this.InnerDictionary;
        }

        public void SetValue(string key, object value)
        {
            if (value == null)
                this.InnerDictionary.Remove(key);
            else
                this.InnerDictionary[key] = value;
        }

        public bool ContainsKey(string key)
        {
            return this.InnerDictionary.ContainsKey(key);
        }
    }
}
