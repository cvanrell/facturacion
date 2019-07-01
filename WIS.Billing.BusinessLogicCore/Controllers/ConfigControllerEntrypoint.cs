using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.GridUtil;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.ServiceWrappers;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.Controllers
{
    public class ConfigControllerEntrypoint
    {
        public async Task UpdateGridConfig(GridWrapper data, CancellationToken cancelToken)
        {
            await Task.Run(() =>
            {
                var configData = data.GetData<GridUpdateConfigRequest>();

                using (WISDB context = new WISDB())
                {
                    GridConfig config = new GridConfig(context, data.Application, configData.GridId, data.User);

                    config.Update(configData.Columns);
                }
            });
        }
    }
}
