using System;
using System.Collections.Generic;
using System.Linq;
using WIS.Billing.DataAccess;
using WIS.Billing.Entities;

namespace WIS.Billing.BusinessLogic
{
    public static class ProjectActions
    {
        public static List<Project> GetProjects(DataContext context)
        {
            List<Project> result = new List<Project>();
            result.AddRange(context.Projects.Include("Client").Include("Fees").ToList());
            return result;
        }

        public static void AddProject(DataContext context, Project project)
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

        public static Project GetProject(DataContext context, Guid projectId)
        {
            return context.Projects.Include("Client").Include("Fees").SingleOrDefault(p => p.Id == projectId);
        }

        public static void UpdateProject(DataContext context, Project project)
        {
            context.Entry(project).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteProject(DataContext context, Guid projectId)
        {
            Project project = context.Projects.Find(projectId);
            context.Projects.Remove(project);
            context.SaveChanges();
        }
    }
}
