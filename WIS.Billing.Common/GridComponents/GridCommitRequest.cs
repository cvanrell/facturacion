using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.App;

namespace WIS.Common.GridComponents
{
    public class GridCommitRequest
    {
        public string GridId { get; set; }
        public GridFetchRequest Query { get; set; }
        public List<GridRow> Rows { get; set; }
    }
}
