using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation
{
    public class ComponentValidationErrorGroup
    {
        public bool IsValid { get; private set; }
        public List<IValidationError> Errors { get; private set; }

        public ComponentValidationErrorGroup()
        {
            this.IsValid = true;
            this.Errors = new List<IValidationError>();
        }

        public void AddErrors(List<IValidationError> errors)
        {
            this.IsValid = false;
            this.Errors.AddRange(errors);
        }

        public string GetMessage()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var error in this.Errors)
            {
                sb.Append(error.Message);
            }

            return sb.ToString();
        }
    }
}
