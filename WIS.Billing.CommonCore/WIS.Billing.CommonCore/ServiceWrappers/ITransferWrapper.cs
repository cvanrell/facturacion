using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.ServiceWrappers
{
    public interface ITransferWrapper
    {
        string Application { get; set; }
        int User { get; set; }
        string Data { get; set; }
        string PageToken { get; set; }
        TransferWrapperStatus Status { get; set; }
        string Message { get; set; }
        string SessionData { get; set; }

        void SetData(Object data);
        T GetData<T>(bool preserveReferences = false);
        void SetSessionData(Object data);
        Dictionary<string, object> GetSessionData();
        /// <summary>
        /// Resuelve las relaciones de objetos dentro del serializado y retorna un objeto sin relaciones
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetResolvedData<T>();
        void SetError(string message);

        ISerializationBinder GetSerializationBinder();
    }
}
