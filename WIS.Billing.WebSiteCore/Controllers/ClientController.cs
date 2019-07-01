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
    public class ClientController : Controller
    {
        private readonly WISDB _context;

        public ClientController(WISDB context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<Client> GetClients()
        {
            return ClientActions.GetClients(_context);
        }

        [HttpPost("[action]")]
        public void Create(Client client)
        {
            ClientActions.AddClient(_context, client);
        }

        [HttpGet("[action]/{id}")]
        public Client Details(Guid id)
        {
            return ClientActions.GetClient(_context, id);
        }

        [HttpPut("[action]")]
        public void Edit(Client client)
        {
            ClientActions.UpdateClient(_context, client);
        }

        [HttpDelete("[action]/{id}")]
        public void Delete(Guid id)
        {
            ClientActions.DeleteClient(_context, id);
        }

    }
}