using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Convierte la fecha nuleable seleccionada en string
        /// </summary>
        /// <param name="value">Fecha de tipo DateTime nuleable</param>
        /// <param name="format">Formato de salida como string</param>
        /// <returns>Fecha formateada como string si la fecha no es nula, un string vacío de lo contrario</returns>
        public static string ToString(this DateTime? value, string format)
        {
            if (value == null)
                return string.Empty;

            DateTime realValue = value ?? DateTime.Now; //Nunca entra en else

            return realValue.ToString(format);
        }
        /// <summary>
        /// Convierte fecha a formato ISO 8601
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIsoString(this DateTime value)
        {
            return value.ToString("O");
        }
        /// <summary>
        /// Convierte fecha a formato ISO 8601
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIsoString(this DateTime? value)
        {
            if (value == null)
                return string.Empty;

            DateTime realValue = value ?? DateTime.Now; //Nunca entra en else

            return realValue.ToString("O");
        }
    }
}
