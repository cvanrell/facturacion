using WIS.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Serialization.Converters;

namespace WIS.Common.ServiceWrappers
{
    public abstract class TransferWrapper : ITransferWrapper
    {
        public string Application { get; set; }
        public int User { get; set; }
        public string Data { get; set; }      
        public string PageToken { get; set; }
        public TransferWrapperStatus Status { get; set; }
        public string Message { get; set; }
        public string SessionData { get; set; }

        public TransferWrapper()
        {
            this.Status = TransferWrapperStatus.Ok;
        }
        public TransferWrapper(ITransferWrapper wrapper)
        {
            this.Application = wrapper.Application;
            this.User = wrapper.User;
            this.Status = TransferWrapperStatus.Ok;
        }
        public TransferWrapper(string application, GridAction action, int user, string pageToken)
        {
            this.Application = application;
            this.User = user;
            this.Status = TransferWrapperStatus.Ok;
            this.PageToken = pageToken;
        }

        public void SetData(Object data)
        {
            var settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                FloatParseHandling = FloatParseHandling.Decimal,
                TypeNameHandling = TypeNameHandling.Objects,
                SerializationBinder = this.GetSerializationBinder()
            };

            this.Data = JsonConvert.SerializeObject(data, settings);
        }
        public T GetData<T>(bool preserveReferences = false)
        {
            if (this.Data == null)
                return default(T);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                SerializationBinder = this.GetSerializationBinder()
            };

            if (preserveReferences)
            {
                settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                settings.FloatParseHandling = FloatParseHandling.Decimal;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return JsonConvert.DeserializeObject<T>(this.Data, settings);
        }
        public void SetSessionData(Object data)
        {
            var settings = new JsonSerializerSettings
            {
                FloatParseHandling = FloatParseHandling.Decimal //TODO: Pasar a configuración de sistema
            };

            settings.Converters.Insert(0, new PrimitiveJsonConverter());

            this.SessionData = JsonConvert.SerializeObject(data, Formatting.None, settings);
        }
        public Dictionary<string, object> GetSessionData()
        {
            if (this.SessionData == null)
                return default(Dictionary<string, object>);

            var settings = new JsonSerializerSettings
            {
                FloatParseHandling = FloatParseHandling.Decimal
            };

            settings.Converters.Insert(0, new PrimitiveJsonConverter());

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(this.SessionData, settings);
        }
        /// <summary>
        /// Resuelve las relaciones de objetos dentro del serializado y retorna un objeto sin relaciones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetResolvedData<T>()
        {
            var data = this.GetData<T>();

            var settings = new JsonSerializerSettings
            {
                FloatParseHandling = FloatParseHandling.Decimal,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var serializedData = JsonConvert.SerializeObject(data, settings);

            return serializedData;
        }
        public void SetError(string message)
        {
            this.Status = TransferWrapperStatus.Error;
            this.Message = message;
        }

        public virtual ISerializationBinder GetSerializationBinder()
        {
            //Esto se define por seguridad, no se permite pasar tipos no esperados
            throw new NotImplementedException();
        }
    }    
}
