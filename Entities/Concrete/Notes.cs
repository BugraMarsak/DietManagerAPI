using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Notes
    {
        public int NotesId { get; set; }
        public string NotesName { get; set; }
        public DateTime? NoteTime { get; set; }
        public string Note { get; set; }
        public int DietianId { get; set; }
        public int ClientId { get; set; }
    }
}
