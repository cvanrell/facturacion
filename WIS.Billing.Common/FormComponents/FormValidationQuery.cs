using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.FormComponents
{
    public class FormValidationQuery
    {
        public string FieldId { get; set; }
        public List<ComponentParameter> Parameters { get; set; }
        public string Redirect { get; set; }

        public FormValidationQuery()
        {
            this.Parameters = new List<ComponentParameter>();
        }
    }
}
