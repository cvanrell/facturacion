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
                typeof(WIS.Common.FormComponents.Form),
                typeof(WIS.Common.FormComponents.FormField),
                typeof(WIS.Common.FormComponents.FormData),
                typeof(WIS.Common.FormComponents.FormValidationData),
                typeof(WIS.Common.FormComponents.FormSubmitData),
                typeof(WIS.Common.FormComponents.FormButtonActionData),
                typeof(WIS.Common.FormComponents.FormQuery),
                typeof(WIS.Common.FormComponents.FormValidationQuery),
                typeof(WIS.Common.FormComponents.FormSubmitQuery),
                typeof(WIS.Common.FormComponents.FormButtonActionQuery),
                typeof(WIS.Common.FormComponents.FormSelectOptions),

                typeof(WIS.Common.App.ComponentParameter)
            });
        }
    }
}
