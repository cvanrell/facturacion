using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.ServiceWrappers;

namespace WIS.BusinessLogicCore.Page.Coordinators
{
    public interface IPageCoordinator
    {
        IPageWrapper Load(IPageWrapper wrapper);
        IPageWrapper Unload(IPageWrapper wrapper);
    }
}
