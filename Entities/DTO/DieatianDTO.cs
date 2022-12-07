using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class DieatianDTO
    {
        public DieatianDTO(string fullName, int clientId)
        {
            DietianId = clientId;
            DietianName = fullName;
        }
        public string DietianName { get; set; }
        public int DietianId { get; set; }

    }
}
