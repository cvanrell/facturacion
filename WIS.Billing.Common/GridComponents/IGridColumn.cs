using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;

namespace WIS.Common.GridComponents
{
    public interface IGridColumn
    {
        string Id { get; set; }
        string Name { get; set; }
        ColumnType Type { get; set; }
        decimal Width { get; set; }
        short Order { get; set; }
        FixPosition Fixed { get; set; }
        bool Insertable { get; set; }
        bool Hidden { get; set; }
        string CssClass { get; set; }
        GridTextAlign LabelAlign { get; set; }
        GridTextAlign TextAlign { get; set; }
        string DefaultValue { get; set; }

        [JsonIgnore]
        bool IsNew { get; set; }

        void UpdateSpecificValues(IGridColumn column);
    }
}
