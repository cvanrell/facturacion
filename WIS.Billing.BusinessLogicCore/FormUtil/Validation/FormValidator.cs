using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.Validation;
using WIS.CommonCore.App;
using WIS.CommonCore.FormComponents;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.FormUtil.Validation
{
    public class FormValidator
    {
        private readonly WISDB _context;
        private readonly int _userId;
        private readonly List<ComponentParameter> _parameters;

        public FormValidationSchema Schema { get; set; }
        public List<FormValidationGroup> Groups { get; set; }

        public FormValidator(WISDB context, List<ComponentParameter> parameters, int userId)
        {
            this._context = context;
            this._userId = userId;
            this._parameters = parameters;
            this.Schema = new FormValidationSchema();
            this.Groups = new List<FormValidationGroup>();
        }

        public void Validate(FormField field, Form form)
        {
            if (this.Schema.Count == 0)
                return;

            this.SetValidationRules(field, form);

            foreach (var validation in this.Groups)
            {
                ComponentValidationErrorGroup result = validation.Validate(field, form, this._parameters, this._userId, this._context);

                if (result.IsValid)
                {
                    validation.Field.SetOk();
                }
                else
                {
                    validation.Field.SetError(result.GetMessage());

                    if (validation.BreakValidationChain)
                        break;
                }
            }
        }
        public void Validate(Form form)
        {
            if (this.Schema.Count == 0)
                return;

            this.SetValidationRules(form);

            foreach (var validation in this.Groups)
            {
                ComponentValidationErrorGroup result = validation.Validate(validation.Field, form, this._parameters, this._userId, this._context);

                if (result.IsValid)
                {
                    validation.Field.SetOk();
                }
                else
                {
                    validation.Field.SetError(result.GetMessage());

                    if (validation.BreakValidationChain)
                        break;
                }
            }
        }

        private void SetValidationRules(FormField field, Form form)
        {
            if (field != null && this.Schema.ContainsKey(field.Id))
            {
                var result = this.Schema[field.Id](field, form, this._parameters, this._userId, this._context);

                if (result == null)
                    return;

                foreach (var dependency in result.Dependencies)
                {
                    var subField = form.GetField(dependency);

                    this.SetValidationRules(subField, form);
                }

                this.Groups.Add(result);
            }
        }
        private void SetValidationRules(Form form)
        {
            foreach(var field in form.Fields)
            {
                if (this.Groups.Any(d => d.Field.Id == field.Id))
                    return;

                this.SetValidationRules(field, form);
            }
        }
    }
}
