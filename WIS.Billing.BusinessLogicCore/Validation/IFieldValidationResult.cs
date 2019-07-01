using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public interface IFieldValidationResult
    {
        bool IsValid { get; set; }
        List<IValidationErrorGroup> ErrorGroups { get; }

        void AddGroup(IValidationErrorGroup errorGroup);
    }
}
