using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.GridComponents
{
    public abstract class GridItem : IGridItem
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string CssClass { get; set; }
        public GridItemType ItemType { get; set; }
    }
}
