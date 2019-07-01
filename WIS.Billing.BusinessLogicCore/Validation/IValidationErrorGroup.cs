using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation
{
    public interface IValidationErrorGroup
    {
        string FieldName { get; set; }
        List<IValidationError> Errors { get; }
        bool IsValid { get; }

        void AddErrors(List<IValidationError> error);
    }
}
