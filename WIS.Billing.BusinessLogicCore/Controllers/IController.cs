using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;

namespace WIS.BusinessLogicCore.Controllers
{
    public interface IController
    {
        PageQueryData PageLoad(PageQueryData data, int userId);
        PageQueryData PageUnload(PageQueryData data, int userId);
    }
}
