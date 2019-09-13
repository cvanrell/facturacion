using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using WIS.CommonCore.GridComponents;

namespace WIS.Billing.BusinessLogicCore.Controllers
{
    class GridHelper
    {
        private static string DATETIME_FORMAT_CORTO = "dd/MM/yyyy";
        private static string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";

        public static T RowToEntity<T>(GridRow row, List<string> colPropeExclude, bool IsOlds = false)
        {
            object Out = Activator.CreateInstance(typeof(T), new object[] { });
            foreach (PropertyInfo prop in Out.GetType().GetProperties())
            {
                if (!colPropeExclude.Contains(prop.Name))
                {
                    string add = row.Cells[0].Column.Name;


                    GridCell cell = row.Cells.FirstOrDefault(x => x.Column.Id == prop.Name);
                    if (cell != null)
                    {
                        string inValue = cell.Value;
                        //if (IsOlds)
                        //    inValue = cell.old_value;

                        object value = ConvertStringToPropertyType(inValue, prop.PropertyType);
                        prop.SetValue(Out, value);
                    }
                }
            }
            return (T)Out;
        }

        protected static object ConvertStringToPropertyType(string valor, Type propertyType)
        {
            try
            {
                Object converted = null;
                //STRING
                if (propertyType == typeof(String))
                    converted = valor;
                // INT 16
                else if (propertyType == typeof(Int16))
                    converted = Int16.Parse(valor);
                else if (propertyType == typeof(Int16?))
                {
                    if (!string.IsNullOrEmpty(valor))
                        converted = (Int16?)Int16.Parse(valor);
                }
                // INT 32                
                if (propertyType == typeof(Int32))
                {
                    if (!String.IsNullOrEmpty(valor))
                    {
                        converted = Int32.Parse(valor);
                    }

                    else if (propertyType == typeof(Int32?))
                    {
                        if (!string.IsNullOrEmpty(valor))
                            converted = (Int32?)Int32.Parse(valor);
                    }
                    //// INT 64
                    //else if (propertyType == typeof(Int64))
                    //    converted = Int64.Parse(valor);
                    //else if (propertyType == typeof(Int64?))
                    //{
                    //    if (!string.IsNullOrEmpty(valor))
                    //        converted = (Int64?)Int64.Parse(valor);
                    //}
                    //// DECIMAL
                    //else if (propertyType == typeof(Decimal))
                    //    converted = Decimal.Parse(valor);
                    //else if (propertyType == typeof(Decimal?))
                    //{
                    //    if (!string.IsNullOrEmpty(valor))
                    //        converted = (Decimal?)Decimal.Parse(valor);
                    //}
                    
                }
                //DATEIME
                else if (propertyType == typeof(DateTime))
                {
                    var ci = new CultureInfo("es-UY");
                    var formatString = "yyyy-MM-dd HH:mm:ss";

                    if (valor.ToString().Length == 10)
                        formatString = "yyyy-MM-dd";
                    if(!string.IsNullOrEmpty(valor))
                    {
                        //converted = DateTime.ParseExact(valor, formatString, ci);
                    }
                    
                }
                else if (propertyType == typeof(DateTime?))
                {
                    if (!string.IsNullOrEmpty(valor))
                    {
                        Exception temp = null;

                        try
                        {
                            var ci = new CultureInfo("es-UY");
                            var formatString = "yyyy-MM-dd HH:mm:ss";

                            if (valor.ToString().Length == 10)
                                formatString = "yyyy-MM-dd";

                            converted = DateTime.ParseExact(valor, formatString, ci);
                        }
                        catch (Exception ex)
                        {
                            temp = ex;
                        }

                        if (temp != null)
                        {
                            try
                            {
                                var ci = new CultureInfo("es-UY");
                                var formatString = DATETIME_FORMAT;

                                if (valor.ToString().Length == 10)
                                    formatString = DATETIME_FORMAT_CORTO;

                                converted = DateTime.ParseExact(valor, formatString, ci);
                            }
                            catch (Exception ex)
                            {
                                throw temp;
                            }
                        }


                    }
                }
                    
                

                // GUID
                if (propertyType == typeof(Guid))
                {
                    if (!String.IsNullOrEmpty(valor))
                    {
                        converted = Guid.Parse(valor);
                    }

                }

                // INT 64
                if (propertyType == typeof(Int64))
                    converted = Int64.Parse(valor);
                else if (propertyType == typeof(Int64?))
                {
                    if (!string.IsNullOrEmpty(valor))
                        converted = (Int64?)Int64.Parse(valor);
                }
                // DECIMAL
                if (propertyType == typeof(Decimal))
                    converted = Decimal.Parse(valor);
                else if (propertyType == typeof(Decimal?))
                {
                    if (!string.IsNullOrEmpty(valor))
                        converted = (Decimal?)Decimal.Parse(valor);
                }
                return converted;
            }
            catch (FormatException ex)
            {
                //LogException(ex, "ConvertStringToPropertyType");
                throw new Exception("Error en conversion de datos: " + ex.Message);
            }
        }
    }
}
