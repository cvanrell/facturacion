using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.FormComponents
{
    public class Form
    {
        public string Id { get; set; }
        public List<FormField> Fields { get; set; }

        public Form()
        {
            this.Fields = new List<FormField>();
        }

        public Form(string id)
        {
            this.Id = id;
            this.Fields = new List<FormField>();
        }

        public FormField GetField(string id)
        {
            return this.Fields.Where(d => d.Id == id).FirstOrDefault();
        }
    }
}
