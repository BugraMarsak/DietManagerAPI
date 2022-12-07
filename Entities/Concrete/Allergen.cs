using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Allergen
    {
        [Key]
        public int Id { get; set; }
        public string AllergenName { get; set; }
    }
}
