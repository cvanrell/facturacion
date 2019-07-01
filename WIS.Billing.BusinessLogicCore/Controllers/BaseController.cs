using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Session;
using WIS.CommonCore.Exceptions;
using WIS.Billing.DataAccessCore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WIS.CommonCore.Page;
using WIS.BusinessLogicCore.GridUtil.Validation;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.FormComponents;
using WIS.BusinessLogicCore.FormUtil.Validation;
using WIS.CommonCore.App;
using WIS.BusinessLogicCore.Controllers;

namespace WIS.BusinessLogicCore.Controllers
{
    public abstract class BaseController : IController, IGridController, IFormController
    {
        #region Page
        public virtual PageQueryData PageLoad(PageQueryData data, int userId)
        {
            return data;
        }
        public virtual PageQueryData PageUnload(PageQueryData data, int userId)
        {
            return data;
        }
        #endregion

        #region Form
        public virtual Form FormInitialize(Form form, FormQuery query, int userId)
        {
            throw new NotImplementedException();
        }
        public virtual Form FormValidateField(Form form, FormValidationQuery query, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var validator = new FormValidator(context, query.Parameters, userId);

                validator.Schema = this.GetValidationSchema(form, query.Parameters, userId, context);

                FormField field = form.GetField(query.FieldId);

                validator.Validate(field, form);
            }

            return form;
        }
        public virtual Form FormValidateForm(Form form, FormSubmitQuery query, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var validator = new FormValidator(context, query.Parameters, userId);

                validator.Schema = this.GetValidationSchema(form, query.Parameters, userId, context);

                validator.Validate(form);
            }

            return form;
        }
        public virtual Form FormButtonAction(Form form, FormButtonActionQuery query, int userId)
        {
            throw new NotImplementedException();
        }
        public virtual Form FormSubmit(Form form, FormSubmitQuery query, int userId)
        {
            throw new NotImplementedException();
        }

        protected virtual FormValidationSchema GetValidationSchema(Form form, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Grid
        public virtual Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            return this.GridFetchRows(service, grid, query, userId);
        }        
        public virtual Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            throw new NotImplementedException();
        }
        public virtual Grid GridValidateRow(IGridService service, GridRow row, Grid grid, List<ComponentParameter> parameters, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var validator = new GridValidator(context, parameters, userId, service);

                validator.Schema = this.GetValidationSchema(service, grid, parameters, userId, context);

                validator.Validate(row);
            }

            return grid;
        }
        public virtual Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            throw new NotImplementedException();
        }
        public virtual GridMenuItemAction GridMenuItemAction(IGridService service, GridMenuItemAction selection, int userId)
        {
            throw new NotImplementedException();
        }
        public virtual GridButtonAction GridButtonAction(IGridService service, GridButtonAction data, int userId)
        {
            throw new NotImplementedException();
        }

        protected virtual GridValidationSchema GetValidationSchema(IGridService service, Grid grid, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            return new GridValidationSchema();
        }
        #endregion
    }
}
