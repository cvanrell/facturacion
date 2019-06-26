using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;
using WIS.CommonCore.Serialization.Binders;

namespace WIS.CommonCore.ServiceWrappers
{
    public class PageWrapper : TransferWrapper, ITransferWrapper, IPageWrapper
    {
        public PageAction Action { get; set; }

        public PageWrapper() : base()
        {
        }

        public override ISerializationBinder GetSerializationBinder()
        {
            //Esto se define por seguridad, no se permite pasar tipos no esperados
            return new CustomSerializationBinder(new List<Type> {
                typeof(WIS.CommonCore.Page.PageQueryData),
                typeof(WIS.CommonCore.App.ComponentParameter)
            });
        }
    }
}
