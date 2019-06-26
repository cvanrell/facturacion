using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Common.Enums;

namespace WIS.Common.ServiceWrappers
{
    public interface IGridWrapper : ITransferWrapper
    {
        GridAction Action { get; set; }
        string GridId { get; set; }
    }
}
