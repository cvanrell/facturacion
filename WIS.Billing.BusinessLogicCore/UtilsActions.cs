using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.EntitiesCore.Enums;

namespace WIS.Billing.BusinessLogicCore
{
    public static class UtilsActions
    {
        public static List<CurrencyInfo> GetCurrencyList()
        {
            List<Currency> currencyList = GetEnumList<Currency>();
            List<CurrencyInfo> result = new List<CurrencyInfo>();
            currencyList.ForEach(currency => result.Add(
                new CurrencyInfo
                {
                    Currency = currency
                }
            ));
            return result;
        }

        public static List<PeriodicityInfo> GetPeriodicityList()
        {
            List<Periodicity> periodicityList = GetEnumList<Periodicity>();
            List<PeriodicityInfo> result = new List<PeriodicityInfo>();
            periodicityList.ForEach(periodicity => result.Add(
                new PeriodicityInfo
                {
                    Periodicity = periodicity
                }
            ));
            return result;
        }

        private static List<T> GetEnumList<T>()
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            List<T> list = new List<T>(array);
            return list;
        }
    }
}
