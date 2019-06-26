using WIS.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Serialization.Converters;

namespace WIS.Common.GridComponents
{
    public class GridCell
    {
        [JsonConverter(typeof(GridCellColumnConverter))]
        public IGridColumn Column { get; set; }

        [JsonIgnore]
        private bool IsOldSet { get; set; }
        [JsonIgnore]
        private string OldValue { get; set; }

        public string Value { get; set; }
        public bool Editable { get; set; }
        public string CssClass { get; set; }
        public GridStatus Status { get; set; }
        public string Message { get; set; }
        public bool Modified { get; set; }
        public string Old {
            get { return this.OldValue; }
            set {
                if (this.IsOldSet) throw new Exception("Valor ya seteado");

                this.IsOldSet = true;

                this.OldValue = value;
            }
        }

        [JsonIgnore]
        public bool IsValidated { get; set; }

        public GridCell()
        {
            this.Column = null;
            this.Value = string.Empty;
            this.Editable = true;
            this.CssClass = null;
            this.Status = GridStatus.Ok;
            this.Message = null;
            this.Modified = false;
            this.IsValidated = false;
        }

        public void SetError(string message)
        {
            this.Status = GridStatus.Error;
            this.Message = message;
            this.IsValidated = true;
        }
        public void SetOk()
        {
            this.Status = GridStatus.Ok;
            this.Message = null;
            this.IsValidated = true;
        }

        public bool ShouldValidate()
        {
            return !this.IsValidated && this.Modified;
        }

        public bool IsValid()
        {
            return this.Status == GridStatus.Ok;
        }
    }    
}
