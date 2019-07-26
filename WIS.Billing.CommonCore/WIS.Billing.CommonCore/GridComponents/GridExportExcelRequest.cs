using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FilterComponents;
using WIS.CommonCore.SortComponents;

namespace WIS.CommonCore.GridComponents
{
    public class GridExportExcelRequest : ComponentQuery
    {
        public string FileName { get; set; }
        public GridExportExcelType Type { get; set; }
        public string GridId { get; set; }
        public string ExplicitFilter { get; set; }
        public List<FilterCommand> Filters { get; set; }
        public List<SortCommand> Sorts { get; set; }

        public GridExportExcelRequest() : base()
        {
            this.Filters = new List<FilterCommand>();
            this.Sorts = new List<SortCommand>();
        }
    }
}
