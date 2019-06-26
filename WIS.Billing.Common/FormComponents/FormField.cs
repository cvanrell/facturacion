using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;

namespace WIS.Common.FormComponents
{
    public class FormField
    {
        public string Id { get; set; }
        public string CssClass { get; set; }
        public bool Hidden { get; set; }
        public bool ReadOnly { get; set; }
        public bool Disabled { get; set; }
        public FormStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> Dependencies { get; set; }
        public string Value { get; set; }
        public List<FormSelectOptions> options { get; set; }

        [JsonIgnore]
        public bool IsValidated { get; set; }

        public FormField()
        {
            this.Status = FormStatus.Ok;
            this.IsValidated = false;
            this.Dependencies = new List<string>();
        }

        public bool IsValid()
        {
            return this.Status == FormStatus.Ok;
        }

        public void SetError(string message)
        {
            this.Status = FormStatus.Error;
            this.ErrorMessage = message;
            this.IsValidated = true;
        }
        public void SetOk()
        {
            this.Status = FormStatus.Ok;
            this.ErrorMessage = null;
            this.IsValidated = true;
        }
    }
}
