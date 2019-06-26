using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.FormComponents
{
    public class FormData
    {
        public Form Form { get; set; }
        public FormQuery Query { get; set; }

        public FormData()
        {
            this.Form = new Form();
            this.Query = new FormQuery();
        }
    }
}
