using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.ServiceWrappers;

namespace WIS.BusinessLogicCore.FormUtil.Coordinators
{
    public interface IFormCoordinator
    {
        IFormWrapper Initialize(IFormWrapper wrapper);
        IFormWrapper ValidateField(IFormWrapper wrapper);
        IFormWrapper ButtonAction(IFormWrapper wrapper);
        IFormWrapper Submit(IFormWrapper wrapper);
        IFormWrapper SelectSearch(IFormWrapper wrapper);
    }
}
