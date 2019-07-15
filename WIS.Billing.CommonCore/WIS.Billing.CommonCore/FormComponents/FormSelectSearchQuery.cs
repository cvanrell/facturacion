using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.FormComponents
{
    public class FormSelectSearchQuery
    {
        public string FieldId { get; set; }
        public string SearchValue { get; set; }
        public List<ComponentParameter> Parameters { get; set; }
    }
}
