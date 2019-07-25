using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.App
{
    public abstract class ComponentQuery
    {
        public List<ComponentParameter> Parameters { get; set; }
        public List<Notification> Notifications { get; set; }

        public ComponentQuery()
        {
            this.Parameters = new List<ComponentParameter>();
            this.Notifications = new List<Notification>();
        }

        public string GetParameter(string parameterId)
        {
            return this.Parameters.Where(d => d.Id == parameterId).FirstOrDefault()?.Value;
        }
        public void AddParameter(string parameterId, string value)
        {
            this.Parameters.Add(new ComponentParameter(parameterId, value));
        }

        public void AddSuccessNotification(string message)
        {
            this.Notifications.Add(new Notification(NotificationType.Success, message));
        }
        public void AddSuccessNotification(string message, List<string> arguments)
        {
            this.Notifications.Add(new Notification(NotificationType.Success, message, arguments));
        }
        public void AddErrorNotification(string message)
        {
            this.Notifications.Add(new Notification(NotificationType.Error, message));
        }
        public void AddErrorNotification(string message, List<string> arguments)
        {
            this.Notifications.Add(new Notification(NotificationType.Error, message, arguments));
        }
        public void AddInfoNotification(string message)
        {
            this.Notifications.Add(new Notification(NotificationType.Info, message));
        }
        public void AddInfoNotification(string message, List<string> arguments)
        {
            this.Notifications.Add(new Notification(NotificationType.Info, message, arguments));
        }
        public void AddWarningNotification(string message)
        {
            this.Notifications.Add(new Notification(NotificationType.Warning, message));
        }
        public void AddWarningNotification(string message, List<string> arguments)
        {
            this.Notifications.Add(new Notification(NotificationType.Warning, message, arguments));
        }
    }
}
