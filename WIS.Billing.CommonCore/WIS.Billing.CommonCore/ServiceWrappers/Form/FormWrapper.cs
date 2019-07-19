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
    public class FormWrapper : TransferWrapper, IFormWrapper
    {
        public FormAction Action { get; set; }
        public string FormId { get; set; }

        public FormWrapper() : base()
        {
        }
        public FormWrapper(IFormWrapper wrapper) : base(wrapper)
        {
            this.FormId = wrapper.FormId;
        }

        public override ISerializationBinder GetSerializationBinder()
        {
            //Esto se define por seguridad, no se permite pasar tipos no esperados
            return new CustomSerializationBinder(new List<Type> {
                typeof(WIS.CommonCore.FormComponents.Form),
                typeof(WIS.CommonCore.FormComponents.FormField),
                typeof(WIS.CommonCore.FormComponents.FormData),
                typeof(WIS.CommonCore.FormComponents.FormValidationData),
                typeof(WIS.CommonCore.FormComponents.FormSubmitData),
                typeof(WIS.CommonCore.FormComponents.FormButtonActionData),
                typeof(WIS.CommonCore.FormComponents.FormQuery),
                typeof(WIS.CommonCore.FormComponents.FormValidationQuery),
                typeof(WIS.CommonCore.FormComponents.FormSubmitQuery),
                typeof(WIS.CommonCore.FormComponents.FormButtonActionQuery),
                typeof(WIS.CommonCore.FormComponents.FormSelectSearchRequest),
                typeof(WIS.CommonCore.FormComponents.FormSelectSearchResponse),
                typeof(WIS.CommonCore.FormComponents.FormSelectSearchQuery),

                typeof(WIS.CommonCore.App.ComponentParameter),
                typeof(WIS.CommonCore.App.SelectOption)
            });
        }
    }
}
