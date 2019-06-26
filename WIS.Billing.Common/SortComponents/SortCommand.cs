using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;

namespace WIS.Common.SortComponents
{
    public class SortCommand
    {
        public string ColumnId { get; set; }
        public SortDirection Direction { get; set; }

        public SortCommand()
        {

        }

        public SortCommand(string columnId, SortDirection direction)
        {
            this.ColumnId = columnId;
            this.Direction = direction;
        }
    }
}
