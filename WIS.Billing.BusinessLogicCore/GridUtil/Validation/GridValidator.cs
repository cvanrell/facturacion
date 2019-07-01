using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.BusinessLogicCore.Validation;
using WIS.CommonCore.App;
using WIS.CommonCore.Exceptions;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Session;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.GridUtil.Validation
{
    public class GridValidator
    {
        private readonly IGridService _service;
        private readonly WISDB _context;
        private readonly int _userId;
        private readonly List<ComponentParameter> _parameters;

        public GridValidationSchema Schema { get; set; }
        public List<GridValidationGroup> Groups { get; set; }

        public GridValidator(WISDB context, List<ComponentParameter> parameters, int userId, IGridService service)
        {
            this._context = context;
            this._userId = userId;
            this._service = service;
            this._parameters = parameters;
            this.Schema = new GridValidationSchema();
            this.Groups = new List<GridValidationGroup>();            
        }

        public void Validate(GridRow row)
        {
            if (this.Schema.Count == 0)
                return;

            foreach(var cell in row.Cells.Where(d => d.ShouldValidate()))
            {
                this.SetValidationRules(cell.Column.Id, row);
            }

            foreach(var validation in this.Groups)
            {
                ComponentValidationErrorGroup result = validation.Validate(this._service, row, this._parameters, this._userId, this._context);

                if (result.IsValid)
                {
                    validation.Cell.SetOk();
                }
                else
                {
                    validation.Cell.SetError(result.GetMessage());

                    if (validation.BreakValidationChain)
                        break;
                }
            }
        }

        private void SetValidationRules(string columnId, GridRow row)
        {
            if (this.Groups.Any(d => d.Cell.Column.Id == columnId))
                return;

            var cell = row.GetCell(columnId);

            if (cell != null && this.Schema.ContainsKey(columnId))
            {
                var result = this.Schema[columnId](this._service, cell, row, this._parameters, this._userId, this._context);

                if (result == null)
                    return;

                foreach (var dependency in result.Dependencies)
                {
                    this.SetValidationRules(dependency, row);
                }

                this.Groups.Add(result);
            }
        }
    }
}
