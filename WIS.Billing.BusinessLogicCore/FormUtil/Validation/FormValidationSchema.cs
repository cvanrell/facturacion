using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;
using WIS.CommonCore.FormComponents;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.FormUtil.Validation
{
    public class FormValidationSchema : Dictionary<string, Func<FormField, Form, List<ComponentParameter>, int, WISDB, FormValidationGroup>>
    {
    }
}
