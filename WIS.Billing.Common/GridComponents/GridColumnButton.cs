using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.GridComponents
{
    public class GridColumnButton : GridColumn
    {
        public List<GridButton> Buttons { get; set; }

        public GridColumnButton() : base()
        {
            this.Type = Enums.ColumnType.Button;
        }

        public GridColumnButton(string id, List<GridButton> buttons) : base()
        {
            this.Id = id;
            this.Type = Enums.ColumnType.Button;
            this.Buttons = buttons;
        }

        public override void UpdateSpecificValues(IGridColumn column)
        {
            var castedColumn = (GridColumnButton)column;

            this.Buttons = castedColumn.Buttons;
        }
    }
}
