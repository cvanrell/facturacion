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
using WIS.Billing.EntitiesCore;
using System.Reflection;
using System.Globalization;

namespace WIS.BusinessLogicCore.Controllers
{
    public abstract class BaseController : IController, IGridController, IFormController
    {
        private static string DATETIME_FORMAT_CORTO = "dd/MM/yyyy";
        private static string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";

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
        public virtual List<SelectOption> FormSelectSearch(Form form, FormSelectSearchQuery query, int userId)
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

        //public virtual Grid GridInitialize(IGridService service, Grid grid, GridInitializeQuery query)
        //{
        //    return this.GridFetchRows(service, grid, query.FetchQuery);
        //}
        public virtual Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            throw new NotImplementedException();
        }
        //public virtual Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest query)
        //{
        //    throw new NotImplementedException();
        //}
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
            // Crear contexto y hacer el insert
            //using (WISDB context = new WISDB())
            //{
            //    foreach(GridRow row in grid.Rows)
            //    {
            //        string[] dataList = new string[] { "" };
            //        Client c = RowToEntity<Client>(row, dataList.ToList());

            //        context.Clients.Add(c);
            //        context.SaveChanges();
            //    }

            //}
            //return grid;
        }
        public virtual GridMenuItemActionQuery GridMenuItemAction(IGridService service, GridMenuItemActionQuery selection, int userId)
        {
            throw new NotImplementedException();
        }
        public virtual GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            throw new NotImplementedException();
        }

        protected virtual GridValidationSchema GetValidationSchema(IGridService service, Grid grid, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            return new GridValidationSchema();
        }

        public virtual Form ExecuteAdjustments(Form form, FormButtonActionQuery query, int userId)
        {
            throw new NotImplementedException();
        }

        #endregion

        //private static T RowToEntity<T>(GridRow row, List<string> colPropeExclude, bool IsOlds = false)
        //{
        //    object Out = Activator.CreateInstance(typeof(T), new object[] { });
        //    foreach (PropertyInfo prop in Out.GetType().GetProperties())
        //    {
        //        if (!colPropeExclude.Contains(prop.Name))
        //        {
        //            string add = row.Cells[0].Column.Name;


        //            GridCell cell = row.Cells.FirstOrDefault(x => x.Column.Name == prop.Name);
        //            if (cell != null)
        //            {
        //                string inValue = cell.Value;
        //                //if (IsOlds)
        //                //    inValue = cell.old_value;

        //                object value = ConvertStringToPropertyType(inValue, prop.PropertyType);
        //                prop.SetValue(Out, value);
        //            }
        //        }
        //    }
        //    return (T)Out;
        //}

        //protected static object ConvertStringToPropertyType(string valor, Type propertyType)
        //{
        //    try
        //    {
        //        Object converted = null;
        //        //STRING
        //        if (propertyType == typeof(String))
        //            converted = valor;
        //        // INT 16
        //        else if (propertyType == typeof(Int16))
        //            converted = Int16.Parse(valor);
        //        else if (propertyType == typeof(Int16?))
        //        {
        //            if (!string.IsNullOrEmpty(valor))
        //                converted = (Int16?)Int16.Parse(valor);
        //        }
        //        // INT 32
        //        if (propertyType == typeof(Int32))
        //            converted = Int32.Parse(valor);
        //        else if (propertyType == typeof(Int32?))
        //        {
        //            if (!string.IsNullOrEmpty(valor))
        //                converted = (Int32?)Int32.Parse(valor);
        //        }
        //        // INT 64
        //        else if (propertyType == typeof(Int64))
        //            converted = Int64.Parse(valor);
        //        else if (propertyType == typeof(Int64?))
        //        {
        //            if (!string.IsNullOrEmpty(valor))
        //                converted = (Int64?)Int64.Parse(valor);
        //        }
        //        // DECIMAL
        //        else if (propertyType == typeof(Decimal))
        //            converted = Decimal.Parse(valor);
        //        else if (propertyType == typeof(Decimal?))
        //        {
        //            if (!string.IsNullOrEmpty(valor))
        //                converted = (Decimal?)Decimal.Parse(valor);
        //        }
        //        // DATEIME
        //        else if (propertyType == typeof(DateTime))
        //        {
        //            var ci = new CultureInfo("es-UY");
        //            var formatString = "yyyy-MM-dd HH:mm:ss";

        //            if (valor.ToString().Length == 10)
        //                formatString = "yyyy-MM-dd";

        //            converted = DateTime.ParseExact(valor, formatString, ci);
        //        }
        //        else if (propertyType == typeof(DateTime?))
        //        {
        //            if (!string.IsNullOrEmpty(valor))
        //            {
        //                Exception temp = null;

        //                try
        //                {
        //                    var ci = new CultureInfo("es-UY");
        //                    var formatString = "yyyy-MM-dd HH:mm:ss";

        //                    if (valor.ToString().Length == 10)
        //                        formatString = "yyyy-MM-dd";

        //                    converted = DateTime.ParseExact(valor, formatString, ci);
        //                }
        //                catch (Exception ex)
        //                {
        //                    temp = ex;
        //                }

        //                if (temp != null)
        //                {
        //                    try
        //                    {
        //                        var ci = new CultureInfo("es-UY");
        //                        var formatString = DATETIME_FORMAT;

        //                        if (valor.ToString().Length == 10)
        //                            formatString = DATETIME_FORMAT_CORTO;

        //                        converted = DateTime.ParseExact(valor, formatString, ci);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        throw temp;
        //                    }
        //                }


        //            }
        //        }
        //        return converted;
        //    }
        //    catch (FormatException ex)
        //    {
        //        //LogException(ex, "ConvertStringToPropertyType");
        //        throw new Exception("Error en conversion de datos: " + ex.Message);
        //    }
        //}
    }
}
