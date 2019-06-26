using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.ServiceWrappers
{
    public interface IPageWrapper : ITransferWrapper
    {
        PageAction Action { get; set; }
    }
}
