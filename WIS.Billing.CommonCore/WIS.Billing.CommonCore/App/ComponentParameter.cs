using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.App
{
    public class ComponentParameter
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public ComponentParameter()
        {

        }
        public ComponentParameter(string id, string value)
        {
            this.Id = id;
            this.Value = value;
        }
    }
}
