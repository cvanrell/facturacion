using WIS.CommonCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WIS.CommonCore.GridComponents
{
    public class GridColumn : IGridColumn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public decimal Width { get; set; }
        public short Order { get; set; }
        public FixPosition Fixed { get; set; }
        public bool Insertable { get; set; }
        public bool Hidden { get; set; }
        public string CssClass { get; set; }
        public GridTextAlign LabelAlign { get; set; }
        public GridTextAlign TextAlign { get; set; }
        public string DefaultValue { get; set; }

        [JsonIgnore]
        public bool IsNew { get; set; }

        public GridColumn()
        {
            this.Type = ColumnType.Text;
            this.Fixed = FixPosition.None;
            this.LabelAlign = GridTextAlign.Left;
            this.TextAlign = GridTextAlign.Left;
            this.Hidden = false;
            this.Insertable = true;
            this.Width = 100;
            this.IsNew = false;
        }

        public virtual void UpdateSpecificValues(IGridColumn column)
        {
            return;
        }
    }
}
