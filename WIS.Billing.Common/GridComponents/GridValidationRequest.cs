using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.GridComponents
{
    public class GridValidationRequest
    {
        public string GridId { get; set; }
        public GridRow Row { get; set; }
        public List<ComponentParameter> Parameters { get; set; }
    }
}
