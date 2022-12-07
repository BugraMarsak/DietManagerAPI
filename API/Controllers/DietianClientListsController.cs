using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTO;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietianClientListsController : Controller
    {
        
        //[HttpGet("getall")]
        //public IActionResult GetAll()
        //{
            
        //}
        [HttpGet("GetById")]
        public IActionResult GetById(int DietianId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                List< DietianClientLists> clientList = context.DietianClients.Where(c => c.DietianId == DietianId).ToList();
               List<ClientListDTO> result = new List<ClientListDTO>();
                
                foreach (var item in clientList)
                {
                   var tempUser =context.Users.FirstOrDefault(u => u.Id == item.ClientId);
                    if (tempUser != null)
                    {
                        result.Add(new ClientListDTO($"{tempUser.FirstName} {tempUser.LastName}",item.ClientId));
                    }
                }
                
                if (clientList != null)
                {

                    return Ok(new SuccessDataResult<List<ClientListDTO>>(result, Messages.Listed));
                }
                return BadRequest();

            }
            
        }
        [HttpPost("update")]
        public IActionResult update(DietianClientLists clientAllergies)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                var uptatedEntity = context.Entry(clientAllergies);
                uptatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                var result = new SuccessResult(Messages.changed);
                return Ok(result);

            }
        }


    }
}
