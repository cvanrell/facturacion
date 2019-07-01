using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WIS.Billing.BusinessLogicCore;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore.Enums;

namespace WIS.Billing.WebSiteCore.Controllers
{
    [Route("api/[controller]")]
    public class UtilsController : Controller
    {
        private readonly WISDB _context;

        public UtilsController(WISDB context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<CurrencyInfo> GetCurrencyList()
        {
            return UtilsActions.GetCurrencyList();
        }

        [HttpGet("[action]")]
        public IEnumerable<PeriodicityInfo> GetPeriodicityList()
        {
            return UtilsActions.GetPeriodicityList();
        }
    }
}