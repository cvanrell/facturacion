using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.FormComponents
{
    public class FormQuery
    {
        public List<ComponentParameter> Parameters { get; set; }

        public FormQuery()
        {
            this.Parameters = new List<ComponentParameter>();
        }
    }
}
