using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.DTO;
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
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("test")]
        public IActionResult GetAll(User user)
        {
            var result = _userService.GetClaims(user);
            
            return Ok(result);
           
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var result = _userService.GetById(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var test = _userService.GetById(user.Id);
            user.PasswordHash = test.Data.PasswordHash;
            user.PasswordSalt = test.Data.PasswordSalt;
            user.Status = test.Data.Status;
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("setDietatian")] // musteri tarafindan yapilmis secim 100 den fazla client almama gibi bir durum ekleyebilirim.
        public IActionResult setDietatian(int DieatianId, int ClientId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                DietianClientLists temp = null;
                temp.DietianId = DieatianId;
                temp.ClientId = ClientId;   
                context.DietianClients.Add(temp);
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }

           
        }

        [HttpGet("getDieatians")]
        public IActionResult getDieatians()
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var x = context.UserOperationClaims.Where(x=>x.OperationClaimId == 1).ToList();
                List<DieatianDTO> dieatianDTOs = new List<DieatianDTO>();
                foreach (var item in x)
                {
                   var temp = context.Users.FirstOrDefault(u => u.Id == item.UserId);
                   var tempdto = new DieatianDTO((temp.FirstName+" "+temp.LastName),item.UserId);
                    dieatianDTOs.Add(tempdto);
                }
                return Ok(new SuccessDataResult<List<DieatianDTO>>(dieatianDTOs, Messages.Listed));
            }
        }

        [HttpGet("getDieatianbyId")]
        public IActionResult getDieatianbyId(int ClientId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var x = context.DietianClients.FirstOrDefault(x=>x.ClientId == ClientId);
                if (x != null)
                {
                    

                    return Ok(new SuccessDataResult<DietianClientLists>(x, Messages.Listed));
                }
                else
                {
                    return NotFound();
                }
            }
        }


        

    }
}
