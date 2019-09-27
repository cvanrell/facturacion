using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;
using WIS.CommonCore.FilterComponents;
using WIS.CommonCore.SortComponents;

namespace WIS.CommonCore.GridComponents
{
    public class GridFetchRequest : ComponentQuery
    {
        public string GridId { get; set; }
        public string ExplicitFilter { get; set; }
        public int RowsToFetch { get; set; }
        public int RowsToSkip { get; set; }
        public List<FilterCommand> Filters { get; set; }
        public List<SortCommand> Sorts { get; set; }
        //public List<ComponentParameter> Parameters { get; set; }

        public GridFetchRequest() : base()
        {
            this.Filters = new List<FilterCommand>();
            this.Sorts = new List<SortCommand>();
            //this.Parameters = new List<ComponentParameter>();
        }

        //public string GetParameter(string parameterId)
        //{
        //    return this.Parameters.Where(d => d.Id == parameterId).FirstOrDefault()?.Value;
        //}
    }
}
