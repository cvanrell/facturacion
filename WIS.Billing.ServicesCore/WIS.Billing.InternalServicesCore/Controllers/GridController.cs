//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using WIS.BusinessLogicCore.Controllers;
//using WIS.BusinessLogicCore.GridUtil;
//using WIS.CommonCore.GridComponents;
//using WIS.CommonCore.ServiceWrappers;
//using WIS.CommonCore.Session;
//using WIS.Billing.DataAccessCore.Database;

//namespace WIS.InternalServices.Controllers
//{
//    public class GridController : BaseController
//    {
//        [HttpPost]
//        public async Task<IHttpActionResult> UpdateConfig(GridWrapper data, CancellationToken cancelToken)
//        {
//            IGridWrapper response = new GridWrapper();

//            try
//            {
//                var controller = new ConfigControllerEntrypoint();

//                controller.UpdateGridConfig(data, cancelToken);
//            }
//            catch (Exception ex)
//            {
//                response.SetError(ex.Message);
//            }

//            return Ok(response);
//        }
//    }
//}
