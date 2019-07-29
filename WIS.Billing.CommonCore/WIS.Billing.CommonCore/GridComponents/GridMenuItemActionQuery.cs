using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;
using WIS.CommonCore.FilterComponents;

namespace WIS.CommonCore.GridComponents
{
    public class GridMenuItemActionQuery : ComponentQuery
    {
        public string GridId { get; set; }
        public string ButtonId { get; set; }

        public GridSelection Selection { get; set; }
        public List<FilterCommand> Filters { get; set; }
        

        public string Redirect { get; set; }

        public GridMenuItemActionQuery() : base()
        {
            this.Filters = new List<FilterCommand>();
        }
    }
}
