using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.GridComponents
{
    public class GridInitializeQuery
    {
        public GridFetchRequest FetchQuery { get; set; }

        public bool IsEditingEnabled { get; set; }
        public bool IsCommitEnabled { get; set; }
        public bool IsRollbackEnabled { get; set; }
        public bool IsAddEnabled { get; set; }
        public bool IsRemoveEnabled { get; set; }

        public GridInitializeQuery()
        {
            this.IsEditingEnabled = false;
            this.IsCommitEnabled = true;
            this.IsRollbackEnabled = true;
            this.IsAddEnabled = true;
            this.IsRemoveEnabled = true;
        }
    }
}
