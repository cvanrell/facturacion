using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WIS.Billing.BusinessLogicCore;
using WIS.Billing.DataAccessCore;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.WebSiteCore.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly DataContext _context;

        public ProjectController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<Project> Index()
        {
            return ProjectActions.GetProjects(_context);
        }

        [HttpPost("[action]")]
        public void Create(Project project)
        {
            ProjectActions.AddProject(_context, project);
        }

        [HttpGet("[action]/{id}")]
        public Project Details(Guid id)
        {
            return ProjectActions.GetProject(_context, id);
        }

        [HttpPut("[action]")]
        public void Edit(Project project)
        {
            ProjectActions.UpdateProject(_context, project);
        }

        [HttpDelete("[action]/{id}")]
        public void Delete(Guid id)
        {
            ProjectActions.DeleteProject(_context, id);
        }

    }
}