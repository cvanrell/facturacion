using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Billing.BusinessLogicCore.DataModel.Mappers
{
    public abstract class Mapper
    {
        public bool MapStringToBoolean(string value)
        {
            return !string.IsNullOrEmpty(value) && value == "S";
        }

        public string MapBooleanToString(bool value)
        {
            return value ? "S" : "N";
        }
    }
}
