using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public  class FoodList : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public int Calories { get; set; }
        public string Allergen { get; set; }
        [NotMapped]
        public string[] AllergenArray { get {
                string[] temp = Allergen.Split(",");
                return temp; } }
    }
}
