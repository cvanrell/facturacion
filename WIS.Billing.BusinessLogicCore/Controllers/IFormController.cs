using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;
using WIS.CommonCore.FormComponents;

namespace WIS.BusinessLogicCore.Controllers
{
    public interface IFormController
    {
        Form FormInitialize(Form form, FormQuery query, int userId);
        Form FormValidateForm(Form form, FormSubmitQuery query, int userId);
        Form FormValidateField(Form form, FormValidationQuery query, int userId);
        Form FormButtonAction(Form form, FormButtonActionQuery query, int userId);
        Form FormSubmit(Form form, FormSubmitQuery query, int userId);
        List<SelectOption> FormSelectSearch(Form form, FormSelectSearchQuery query, int userId);
    }
}
