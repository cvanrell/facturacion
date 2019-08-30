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
    public class MaintenanceController : Controller
    {
        //private readonly WISDB _context;

        //public MaintenanceController(WISDB context)
        //{
        //    _context = context;
        //}

        //[HttpGet("[action]")]
        //public IEnumerable<Maintenance> GetMaintenances()
        //{
        //    return MaintenanceActions.GetMaintenances(_context);
        //}
        

        //[HttpPost("[action]")]
        //public void Create(Maintenance maintenance)
        //{
        //    MaintenanceActions.AddMaintenance(_context, maintenance);
        //}

        //[HttpGet("[action]/{id}")]
        //public Maintenance Details(Guid id)
        //{
        //    return MaintenanceActions.GetMaintenance(_context, id);
        //}

        //[HttpPut("[action]")]
        //public void Edit(Maintenance maintenance)
        //{
        //    MaintenanceActions.UpdateMaintenance(_context, maintenance);
        //}

        //[HttpDelete("[action]/{id}")]
        //public void Delete(Guid id)
        //{
        //    MaintenanceActions.DeleteMaintenance(_context, id);
        //}

    }
}