using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public class ValidationErrorGroup : IValidationErrorGroup
    {
        public string FieldName { get; set; }
        public List<IValidationError> Errors { get; private set; }
        public bool IsValid { get; private set; }

        public ValidationErrorGroup(string fieldName)
        {
            this.FieldName = fieldName;
            this.Errors = new List<IValidationError>();
            this.IsValid = false;
        }

        public void AddErrors(List<IValidationError> errors)
        {
            this.IsValid = false;
            this.Errors.AddRange(errors);
        }
    }
}
