using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;
using WIS.Common.FilterComponents;

namespace WIS.Common.GridComponents
{
    public class GridMenuItemAction
    {
        public string GridId { get; set; }
        public string ButtonId { get; set; }

        public GridSelection Selection { get; set; }
        public List<FilterCommand> Filters { get; set; }
        public List<ComponentParameter> Parameters { get; set; }

        public string Redirect { get; set; }
    }
}
