﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.GridComponents
{
    public interface IGridItem
    {
        string Id { get; set; }
        string Label { get; set; }
        string CssClass { get; set; }
        GridItemType ItemType { get; set; }
    }
}
