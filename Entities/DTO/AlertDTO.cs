using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class AlertDTO
    {
        public AlertDTO(string alertname,int senderId,string senderFullName,DateTime alertTime)
        {
            AlertName=alertname;
            SenderId=senderId;
            SenderFullName=senderFullName;
            AlertTime=  alertTime;
        }
        public string AlertName { get; set; }
        public int SenderId { get; set; }
        public string SenderFullName { get; set; }
        public DateTime AlertTime { get; set; }
    }
}
