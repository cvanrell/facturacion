using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.GridComponents
{
    public class GridSelection
    {
        public bool IsInverted { get; set; }
        public List<string> Keys { get; set; }

        public GridSelection()
        {
            this.Keys = new List<string>();
        }
    }
}
