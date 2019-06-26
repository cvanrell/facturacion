using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.GridComponents
{
    public class GridButton : GridItem, IGridItem
    {
        public GridButton()
        {
            this.ItemType = GridItemType.Button;
        }
        public GridButton(string id, string label)
        {
            this.Id = id;
            this.Label = label;
            this.ItemType = GridItemType.Button;
        }
        public GridButton(string id, string label, string cssClass)
        {
            this.Id = id;            
            this.Label = label;
            this.CssClass = cssClass;
            this.ItemType = GridItemType.Button;
        }
    }
}
