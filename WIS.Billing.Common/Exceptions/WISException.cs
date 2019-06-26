using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.Exceptions
{
    public class WISException : Exception
    {
        public string[] StrArguments { get; set; }
        public string Property { get; set; }
        
        public string GetMessage()
        {
            return Message;
        }

        public WISException(string texto, string[] strArguments = null) : base(texto)
        {
            this.StrArguments = strArguments;
            this.Property = string.Empty;
        }

        public static string GetExceptionMessage(Exception ex)
        {
            string msg = string.Empty;

            if (ex.InnerException != null && ex.InnerException.InnerException != null)
                msg = ex.InnerException.InnerException.Message;

            if (string.IsNullOrEmpty(msg) && ex.InnerException != null)
                msg = ex.InnerException.Message;

            if (string.IsNullOrEmpty(msg))
                msg = ex.Message;

            return msg;
        }
    }
}
