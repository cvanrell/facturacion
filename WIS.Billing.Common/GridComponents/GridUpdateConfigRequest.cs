using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.GridComponents
{
    public class GridUpdateConfigRequest
    {
        public string GridId { get; set; }
        public List<GridColumn> Columns { get; set; }
        public List<ComponentParameter> Parameters { get; set; }

        public GridUpdateConfigRequest()
        {
            this.Columns = new List<GridColumn>();
            this.Parameters = new List<ComponentParameter>();
        }
    }
}
