using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DietianId { get; set; }
        public int ClientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        [NotMapped]
        public string? FullName { get; set; }
    }
}
