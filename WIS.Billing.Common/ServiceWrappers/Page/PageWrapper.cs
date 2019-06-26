using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;
using WIS.Common.Serialization.Binders;

namespace WIS.Common.ServiceWrappers
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
                typeof(WIS.Common.Page.PageQueryData),
                typeof(WIS.Common.App.ComponentParameter)
            });
        }
    }
}
