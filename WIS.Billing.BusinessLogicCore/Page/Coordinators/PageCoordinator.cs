using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.Page.Coordinators;
using WIS.CommonCore.Exceptions;
using WIS.CommonCore.Page;
using WIS.CommonCore.ServiceWrappers;
using WIS.CommonCore.Session;

namespace WIS.BusinessLogic.Page.Coordinators
{
    public class PageCoordinator : IPageCoordinator
    {
        private readonly IController _controller;
        private readonly IDbConnection _connection;

        public PageCoordinator(IController controller, IDbConnection connection)
        {
            this._controller = controller;
            this._connection = connection;
        }

        public IPageWrapper Load(IPageWrapper wrapper)
        {
            var query = wrapper.GetData<PageQueryData>();            

            IPageWrapper response = new PageWrapper();

            try
            {
                query = _controller.PageLoad(query, wrapper.User);

                response.SetData(query);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IPageWrapper Unload(IPageWrapper wrapper)
        {
            var query = wrapper.GetData<PageQueryData>();

            IPageWrapper response = new PageWrapper();

            try
            {
                query = _controller.PageUnload(query, wrapper.User);

                response.SetData(query);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
    }
}
