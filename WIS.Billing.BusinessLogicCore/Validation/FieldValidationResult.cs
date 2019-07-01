using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public class FieldValidationResult : IFieldValidationResult
    {
        public bool IsValid { get; set; }
        public List<IValidationErrorGroup> ErrorGroups { get; private set; }

        public FieldValidationResult()
        {
            this.IsValid = true;
            this.ErrorGroups = new List<IValidationErrorGroup>();
        }

        public void AddGroup(IValidationErrorGroup errorGroup)
        {
            this.IsValid = false;
            this.ErrorGroups.Add(errorGroup);
        }
    }
}
