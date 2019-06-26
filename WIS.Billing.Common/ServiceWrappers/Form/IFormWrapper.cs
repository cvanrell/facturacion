using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;

namespace WIS.Common.ServiceWrappers
{
    public interface IFormWrapper : ITransferWrapper
    {
        FormAction Action { get; set; }
        string FormId { get; set; }
    }
}
