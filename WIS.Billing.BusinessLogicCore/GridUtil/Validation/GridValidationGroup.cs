using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.BusinessLogicCore.Validation;
using WIS.CommonCore.App;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Session;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.GridUtil.Validation
{
    public class GridValidationGroup
    {
        public GridCell Cell { get; set; } //TODO: Ver si es posible quitar esta referencia de aca
        public List<string> Dependencies { get; set; }
        public List<IValidationRule> Rules { get; set; }
        public bool BreakValidationChain { get; set; }
        public Action<IGridService, GridCell, GridRow, List<ComponentParameter>, int, WISDB> OnSuccess { private get; set; }
        public Action<IGridService, GridCell, GridRow, List<ComponentParameter>, int, WISDB> OnFailure { private get; set; }

        public GridValidationGroup()
        {
            this.Rules = new List<IValidationRule>();
            this.Dependencies = new List<string>();
            this.BreakValidationChain = false;
        }

        public ComponentValidationErrorGroup Validate(IGridService service, GridRow row, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            var errorGroup = new ComponentValidationErrorGroup();

            foreach (var rule in this.Rules)
            {
                var result = rule.Validate();

                if (result.Count > 0)
                {
                    //Rompe la cadena de validación si una de las validaciones falla
                    errorGroup.AddErrors(result);
                    break;
                }
            }

            if (errorGroup.IsValid && this.OnSuccess != null)
                this.OnSuccess(service, this.Cell, row, parameters, userId, context);

            if (!errorGroup.IsValid && this.OnFailure != null)
                this.OnFailure(service, this.Cell, row, parameters, userId, context);

            return errorGroup;
        }
    }
}
