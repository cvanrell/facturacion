using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;
using WIS.CommonCore.Serialization.Converters;

namespace WIS.CommonCore.GridComponents
{
    public class GridButtonActionQuery
    {
        public string GridId { get; set; }
        public string ButtonId { get; set; }

        [JsonConverter(typeof(GridCellColumnConverter))]
        public GridColumn Column { get; set; }

        public GridRow Row { get; set; }
        public List<ComponentParameter> Parameters { get; set; }

        public string Redirect { get; set; }

        public GridButtonActionQuery() : base()
        {
        }
    }
}
