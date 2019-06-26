using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.FormComponents
{
    public class FormSubmitQuery
    {
        public List<ComponentParameter> Parameters { get; set; }
        public string Redirect { get; set; }
        public bool ResetForm { get; set; }
    }
}
