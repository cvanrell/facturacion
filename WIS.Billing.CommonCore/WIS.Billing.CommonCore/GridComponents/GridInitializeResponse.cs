﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.GridComponents
{
    public class GridInitializeResponse: ComponentQuery
    {
        public Grid Grid { get; set; }
        //public List<ComponentParameter> Parameters { get; set; }
        public GridFilterData FilterData { get; set; }
        public bool IsEditingEnabled { get; set; }
        public bool IsCommitEnabled { get; set; }
        public bool IsRollbackEnabled { get; set; }
        public bool IsAddEnabled { get; set; }
        public bool IsRemoveEnabled { get; set; }

        public GridInitializeResponse() : base()
        {
        }
    }
}
