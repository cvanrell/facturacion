using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.BusinessLogicCore
{
    public static class ProjectActions
    {
        public static List<Project> GetProjects(WISDB context)
        {
            List<Project> result = new List<Project>();
            result.AddRange(context.Projects.Include("Client").Include("Fees").ToList());
            return result;
        }

        public static void AddProject(WISDB context, Project project)
        {
            project.Id = Guid.NewGuid();
            var client = context.Clients.SingleOrDefault(c => c.Id == project.Client.Id);
            if (client == null)
            {
                throw new Exception("El cliente no existe");
            }
            else
            {
                project.Client = client;
            }
            context.Projects.Add(project);
            context.SaveChanges();
        }

        public static Project GetProject(WISDB context, Guid projectId)
        {
            return context.Projects.Include("Client").Include("Fees").SingleOrDefault(p => p.Id == projectId);
        }

        public static void UpdateProject(WISDB context, Project project)
        {
            //context.Entry(project).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteProject(WISDB context, Guid projectId)
        {
            Project project = context.Projects.Find(projectId);
            context.Projects.Remove(project);
            context.SaveChanges();
        }
    }
}
