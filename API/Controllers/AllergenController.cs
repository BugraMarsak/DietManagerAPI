using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenController : Controller
    {
      
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                 
                var result =  new SuccessDataResult<List<Allergen>>(context.Allergen.ToList(), Messages.Listed);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            
        }


    }
}
