using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;

namespace WIS.Common.ServiceWrappers
{
    public interface IPageWrapper : ITransferWrapper
    {
        PageAction Action { get; set; }
    }
}
