using System;
using System.Collections.Generic;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.Billing.BusinessLogicCore.DataModel.Repositories
{
    public class ProjectRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly ProjectMapper _mapper;

        public ProjectRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new ProjectMapper();
        }


        public void AddProject(WISDB context, Project p)
        {
            Client client = context.Clients.FirstOrDefault(x => x.Id == p.Id);
            Project project = context.Projects.FirstOrDefault(x => x.Description == p.Description);
            if (project != null)
            {
                if (project.FL_DELETED == "S")
                {
                    project.FL_DELETED = "N";
                }
                else
                {
                    throw new Exception("Ya existe un proyecto con la descripción especificada");
                }
            }
            else
            {
                project = new Project()
                {
                    Description = p.Description,
                    Currency = p.Currency,
                    Amount = p.Amount,
                    IVA = p.IVA,
                    Total = p.Total,
                    InitialDate = p.InitialDate,
                    TotalAmount = p.Total,
                    FL_DELETED = "N",
                };
                //Pregunto si el cliente para ese proyecto es extranjero y seteo el IVA en 0
                if (client.FL_FOREIGN == "S")
                {
                    project.IVA = 0;
                }
                context.Projects.Add(project);
                context.SaveChanges();
            }
        }

        public void UpdateProject(WISDB context, Project p)
        {
            Project project = context.Projects.FirstOrDefault(x => x.Id == p.Id);
            if (project == null)
            {
                throw new Exception("No se encuentra el proyecto especificado");
            }
            else
            {
                project.Description = p.Description;
                project.Currency = p.Currency;
                project.Amount = p.Amount;
                project.IVA = p.IVA;
                project.Total = p.Total;
                project.InitialDate = p.InitialDate;
                project.TotalAmount = p.TotalAmount;
                context.SaveChanges();
            }
        }

        public void DeleteProject(WISDB context, Project p)
        {
            Project project = context.Projects.FirstOrDefault(x => x.Id == p.Id);
            if (project == null)
            {
                throw new Exception("No se encuentra el proyecto que desea eliminar");
            }
            else
            {
                project.FL_DELETED = "S";
                context.SaveChanges();
            }
        }
    }
}
