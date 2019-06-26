using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.GridComponents
{
    public class GridColumnProgress : GridColumn
    {
        public GridColumnProgress()
        {
            this.Type = Enums.ColumnType.Progress;
        }

        public GridColumnProgress(string id)
        {
            this.Id = id;
            this.Type = Enums.ColumnType.Progress;
        }
    }
}
