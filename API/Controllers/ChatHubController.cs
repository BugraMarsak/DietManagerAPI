
using API.HubLists;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHubController : Controller
    {
        private readonly IHubContext<MessageHub> _hub;


        public ChatHubController(IHubContext<MessageHub> hub)
        {
            _hub = hub;
        }
        [HttpPost("getall")]
        public IActionResult GetAll(MessageDTO messageDTO)
        {
            //_hub.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok("123bugra");    

        }
        [HttpGet("getMessages")]
        public IActionResult GetMessages(int receiverId, int senderId )
        {
            List<Message> changed = new List<Message>();
            List<Message> messages = new List<Message>();
            using (DietManagerContext context = new DietManagerContext())
            {
                changed = context.Messages.Where(m=> m.SenderId == receiverId && m.ReceiverId == senderId && m.IsreciverRead== false).ToList();
                foreach (var item in changed)
                {
                    item.IsreciverRead = true;
                    context.Entry(item);
                }
                context.SaveChanges();
                messages =  context.Messages.Where(m=>(m.SenderId==senderId && m.ReceiverId == receiverId)|| (m.SenderId == receiverId && m.ReceiverId == senderId)).OrderBy(o=>o.SendTime).ToList();
            }

                return Ok(messages);

        }

        [HttpGet("getAlerts")]
        public IActionResult s(int userId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                List<AlertDTO> alerts = new List<AlertDTO>(); 
                var x = context.Messages.Where(m => m.ReceiverId == userId && m.IsreciverRead == false).ToList();
                x = x.DistinctBy(d => d.SenderId).ToList();
                foreach (var item in x)
                {
                    string fullname = getFullName(item.SenderId);
                    alerts.Add(new AlertDTO($"New Mesage From {fullname}", item.SenderId, fullname, item.SendTime));
                }
                var result = new SuccessDataResult<List<AlertDTO>>(alerts, Messages.Listed);
                return Ok(result);
            }
        }


        private string getFullName(int UserId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var tempData = context.Users.FirstOrDefault(f => f.Id == UserId);
                return $"{tempData.FirstName} {tempData.LastName}";
            }   
        }




    }
}
