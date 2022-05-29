using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class NotificationsDto
    {
        //overall notification count
        public int NotificationCount { get; set; }
        //companies notifications
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public string ReqWorkerType { get; set; }
        //workers notifications
        public string WorkerName { get; set; }
        public string WorkerType { get; set; }
        public string WorkerLocation { get; set; }


    }
}
