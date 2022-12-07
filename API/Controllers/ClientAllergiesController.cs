using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Concrete;
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
    public class ClientAllergiesController : Controller
    {
      
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                 
                var result =  new SuccessDataResult<List<ClientAllergies>>(context.ClientAllergies.ToList(), Messages.Listed);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }  
        }

        [HttpGet("getbyId")]
        public IActionResult GetbyId(int userId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var CAData = context.ClientAllergies.FirstOrDefault(c => c.ClientId == userId);
                var result = new SuccessDataResult<ClientAllergies>(CAData, Messages.Listed);

                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }
        [HttpPost("add")]
        public IActionResult add(ClientAllergies clientAllergies)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                context.ClientAllergies.Add(clientAllergies);
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);
            }
        }

        [HttpPost("update")] 
        public IActionResult update(ClientAllergies clientAllergies)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                    var uptatedEntity = context.Entry(clientAllergies);
                    uptatedEntity.State = EntityState.Modified;
                    context.SaveChanges();
                    var result = new SuccessResult(Messages.added);
                    return Ok(result);

            }
        }

    }
}
