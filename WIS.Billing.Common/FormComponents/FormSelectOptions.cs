using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.FormComponents
{
    public class FormSelectOptions
    {
        public string value;
        public string description;

        public FormSelectOptions(string value,string description)
        {
            this.value = value;
            this.description = description;
        }
    }
}
