using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WIS.BusinessLogic.GridUtil;
using WIS.Common.GridComponents;
using WIS.Common.ServiceWrappers;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.Controllers
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
