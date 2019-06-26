using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.GridComponents
{
    public class GridValidationResponse
    {
        public GridRow Row { get; set; }
        public List<ComponentParameter> Parameters { get; set; }
    }
}
