using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.GridComponents
{
    public class GridCommitRequest
    {
        public string GridId { get; set; }
        public GridFetchRequest Query { get; set; }
        public List<GridRow> Rows { get; set; }
    }
}
