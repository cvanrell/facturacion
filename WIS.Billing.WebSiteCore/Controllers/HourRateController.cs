using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WIS.Billing.BusinessLogicCore;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.WebSiteCore.Controllers
{
    [Route("api/[controller]")]
    public class HourRateController: Controller
    {
        private readonly WISDB _context;

        public HourRateController(WISDB context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<HourRate> GetHourRates()
        {
            return HourRateActions.GetHourRates(_context);
        }

        [HttpPost("[action]")]
        public void Create(HourRate hourRate)
        {
            HourRateActions.AddHourRate(_context, hourRate);
        }

        [HttpGet("[action]/{id}")]
        public HourRate Details(Guid id)
        {
            return HourRateActions.GetHourRate(_context, id);
        }

        [HttpPut("[action]")]
        public void Edit(HourRate hourRate)
        {
            HourRateActions.UpdateHourRate(_context, hourRate);
        }

        [HttpDelete("[action]/{id}")]
        public void Delete(Guid id)
        {
            HourRateActions.DeleteHourRate(_context, id);
        }
    }
}
