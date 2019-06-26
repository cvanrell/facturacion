using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.FormComponents
{
    public class FormSubmitQuery
    {
        public List<ComponentParameter> Parameters { get; set; }
        public string Redirect { get; set; }
        public bool ResetForm { get; set; }
    }
}
