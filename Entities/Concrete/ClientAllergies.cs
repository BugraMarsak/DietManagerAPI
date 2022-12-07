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
    public class ClientAllergies : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string AllergiesList { get; set; }
        [NotMapped]
        public string[] AllergenArray
        {
            get
            {
                string[] temp = AllergiesList.Split(",");
                return temp;
            }
         
        }
        //[NotMapped]
        //public string?[] Allergenstr
        //{
        //    get; set;
        //}


    }
}
