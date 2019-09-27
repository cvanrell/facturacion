using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.FilterComponents
{
    public class FilterCommand
    {
        public string ColumnId { get; set; }
        public string Value { get; set; }

        public FilterCommand()
        {

        }

        public FilterCommand(string columnId, string value)
        {
            this.ColumnId = columnId;
            this.Value = value;
        }
    }
}
