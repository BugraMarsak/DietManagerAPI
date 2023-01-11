using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.HubLists
{
    public class MessageHub : Hub
    {

        private DietManagerContext _context;
        
        public void SendMessage1(Message message)
        {
            message.SendTime = DateTime.Now;

            using (DietManagerContext context = new DietManagerContext())
            {
                context.Messages.Add(message);
                context.SaveChanges();
            }
            Clients.All.SendAsync($"ReceiveMessage{message.SenderId}-{message.ReceiverId}", message);
            Clients.All.SendAsync($"ReceiveMessage{message.ReceiverId}-{message.SenderId}", message);      
        }

        public void MessageReaded(Message message)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var temp = checkMessage(message);
                message.IsreciverRead = true;
                var uptatedEntity = context.Entry(message);
                uptatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private bool checkMessage(Message message)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var x = context.Messages.FirstOrDefault(m=>m.MessageId == message.MessageId);
                if (x != null) { 
                    return true;    

                }
                else
                {
                    return false;
                }
            }
        }

    }
}
