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
    public class ClientDietList : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DietitianId { get; set; }
        public string DietInfo { get; set; }
        public DateTime DietDate { get; set; }
        public string Note { get; set; }
        public int Session { get; set; }
        [NotMapped]
        public string[] FoodNames { get; set; }
    }
}
