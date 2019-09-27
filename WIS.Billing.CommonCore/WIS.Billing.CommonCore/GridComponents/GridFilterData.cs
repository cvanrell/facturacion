using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.FilterComponents;
using WIS.CommonCore.SortComponents;

namespace WIS.CommonCore.GridComponents
{
    public class GridFilterData
    {
        public long Id { get; set; }
        public string GridId { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public List<FilterCommand> Filters { get; set; }
        public List<SortCommand> Sorts { get; set; }
        public string ExplicitFilter { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsDefault { get; set; }

        public GridFilterData()
        {
            this.Filters = new List<FilterCommand>();
            this.Sorts = new List<SortCommand>();
        }
    }
}
