﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.GridComponents
{
    public class GridFetchResponse
    {
        public List<GridRow> Rows { get; set; }
        public List<ComponentParameter> Parameters { get; set; }
    }
}
