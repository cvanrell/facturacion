using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.App
{
    public class Notification
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public List<string> Arguments { get; set; }

        public Notification()
        {
        }
        public Notification(NotificationType type, string message)
        {
            this.Type = type;
            this.Message = message;
        }
        public Notification(NotificationType type, string message, List<string> arguments)
        {
            this.Type = type;
            this.Message = message;
            this.Arguments = arguments;
        }
    }
}
