using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.Billing.EntitiesCore.Entities;
using WIS.Billing.EntitiesCore.LogsEntities;

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

        #region PROYECTOS

        public void AddProject(Project p)
        {
            //Client client = _context.Clients.FirstOrDefault(x => x.Id == p.Client.Id);
            Project project = CheckIfProjectExists(p);
            if (project != null)
            {
                if (project.FL_DELETED == "S")
                {
                    project.FL_DELETED = "N";
                    project.DT_UPDROW = DateTime.Now;

                    this._context.SaveChanges();

                    LogProject(project, "UPDATE");
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
                    Client = p.Client,
                    Amount = p.Amount,
                    IVA = p.IVA,
                    Total = p.Total,
                    InitialDate = p.InitialDate,
                    TotalAmount = p.TotalAmount,
                    FL_DELETED = "N",
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                };

                if (int.Parse(p.Currency) == 0)
                {
                    project.Currency = "Dólares";
                }
                else if (int.Parse(p.Currency) == 1)
                {
                    project.Currency = "Pesos";
                }

                //Pregunto si el cliente para ese proyecto es extranjero y seteo el IVA en 0
                if (project.Client.FL_FOREIGN == "S")
                {
                    project.IVA = 0;
                }
                this._context.Projects.Add(project);
                this._context.SaveChanges();
                LogProject(project, "INSERT");
            }
        }

        public void UpdateProject(Project p)
        {
            Project project = _context.Projects.Include(x => x.Client).FirstOrDefault(x => x.Id == p.Id);
            if (project == null)
            {
                throw new Exception("No se encuentra el proyecto especificado");
            }
            else
            {
                project.Description = p.Description;
                //project.Currency = p.Currency;
                project.Amount = p.Amount;
                project.IVA = p.IVA;
                project.Total = p.Total;
                project.InitialDate = p.InitialDate;
                project.TotalAmount = p.TotalAmount;
                project.DT_UPDROW = DateTime.Now;

                if (project.Currency != p.Currency)
                {
                    if (int.Parse(p.Currency) == 0)
                    {
                        project.Currency = "Dólares";
                    }
                    else if (int.Parse(p.Currency) == 1)
                    {
                        project.Currency = "Pesos";
                    }
                }

                _context.SaveChanges();

                LogProject(project, "UPDATE");
            }
        }

        public void DeleteProject(Project p)
        {
            Project project = _context.Projects.Include(x => x.Client).FirstOrDefault(x => x.Id == p.Id);
            if (project == null)
            {
                throw new Exception("No se encuentra el proyecto que desea eliminar");
            }
            else
            {
                project.FL_DELETED = "S";
                project.DT_UPDROW = DateTime.Now;
                _context.SaveChanges();

                LogProject(project, "DELETE");
            }
        }

        #endregion

        public Project CheckIfProjectExists(Project p)
        {
            Project pro = this._context.Projects.Include(x => x.Client).FirstOrDefault(x => x.Description == p.Description && x.Client == p.Client);
            return pro;
        }

        #region LOGS

        public void LogProject(Project p, string action)
        {
            Project project = CheckIfProjectExists(p);
            //Client c;
            ProjectLogObject pLog = new ProjectLogObject()
            {
                Id = project.Id.ToString(),
                Description = project.Description,
                Currency = project.Currency,
                Amount = project.Amount,
                IVA = project.IVA,
                Total = project.Total,
                InitialDate = project.InitialDate,
                TotalAmount = project.TotalAmount,
                FL_DELETED = project.FL_DELETED,
                DT_ADDROW = project.DT_ADDROW,
                DT_UPDROW = project.DT_UPDROW,
                ClientLogObject = new ClientLogObject()
                {
                    Id = project.Client.Id.ToString(),
                    Description = project.Client.Description,
                    RUT = project.Client.RUT,
                    Address = project.Client.Address,
                    FL_DELETED = project.Client.FL_DELETED,
                    FL_FOREIGN = project.Client.FL_FOREIGN,
                    DT_ADDROW = project.Client.DT_ADDROW,
                    DT_UPDROW = project.Client.DT_UPDROW,
                }
            };

            string json = JsonConvert.SerializeObject(pLog);

            T_LOG_PROJECT l = new T_LOG_PROJECT()
            {
                ID_PROJECT = p.Id.ToString(),
                USER = _userId,
                DT_ADDROW = DateTime.Now,
                ACTION = action,
                DATA = json,
                PAGE = "PRO010"
            };

            this._context.T_LOG_PROJECT.Add(l);
        }

        #endregion
    }
}
