using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.FormComponents
{
    public class FormQuery
    {
        public List<ComponentParameter> Parameters { get; set; }

        public FormQuery()
        {
            this.Parameters = new List<ComponentParameter>();
        }

        public string GetParameter(string parameterId)
        {
            return this.Parameters.Where(d => d.Id == parameterId).FirstOrDefault()?.Value;
        }
    }
}
