using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.Validation.Rules
{
    public class StringToDateTimeValdiationRule : IValidationRule
    {
        private readonly string _valueDateString;
        private string DATETIME_24H = "dd/MM/yyyy HH:mm:ss";
        private string DATETIME_24HTT = "dd/MM/yyyy HH:mm:ss tt";
        private string DATE_ONLY = "dd/MM/yyyy";
        private string DATETIME_HHmm = "dd/MM/yyyy HH:mm";
        private string DATETIME_HMMSS = "dd/MM/yyyy h:mm:ss";

        public StringToDateTimeValdiationRule(string valueDateString)
        {
            this._valueDateString = valueDateString;
        }

        public List<IValidationError> Validate()
        {
            var errors = new List<IValidationError>();

            var currCulture = CultureInfo.CurrentCulture;

            var formatString = DATETIME_24H; //"dd/MM/yyyy HH:mm:ss";

            if (this._valueDateString.Length == 10)
                formatString = DATE_ONLY;// "dd/MM/yyyy";

            if (this._valueDateString.Length == 16)
                formatString = DATETIME_HHmm;// "dd/MM/yyyy HH:mm";

            if (this._valueDateString.Length == 24)
                formatString = DATETIME_24HTT; //"dd/MM/yyyy HH:mm:ss tt";

            if (this._valueDateString.Length == 18)
                formatString = DATETIME_HMMSS;// ""dd/MM/yyyy h:mm:ss";

            try
            {
                DateTime.ParseExact(this._valueDateString, formatString, currCulture);
            }
            catch
            {
                errors.Add(new ValidationError("Fecha o formato inválido"));
            }

            return errors;
        }
    }
}
