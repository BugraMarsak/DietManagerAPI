using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ClientListDTO
    {
        public ClientListDTO(string fullName,int clientId)
        {
            ClientId = clientId;
            ClientFullName = fullName;  
        }
        public string ClientFullName{ get; set; }
        public int ClientId { get; set; }
        
    }
}
