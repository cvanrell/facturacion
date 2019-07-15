using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.App
{
    public class SelectOption
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public SelectOption()
        {
        }
        public SelectOption(string value, string label)
        {
            this.Label = label;
            this.Value = value;
        }
    }
}
