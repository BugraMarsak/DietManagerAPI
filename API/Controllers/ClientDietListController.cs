using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDietListController : Controller
    {
        IClientDietListService _clientDietListService;

        public ClientDietListController(IClientDietListService clientDietListService)
        {
            _clientDietListService = clientDietListService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _clientDietListService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int UserId)
        {
            var result = _clientDietListService.GetByUserId(UserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByClientIdAndDietianId")]
        public IActionResult GetByClientIdAndDietianId(int ClientId, int DietianId)
        {
            var result = _clientDietListService.GetByUserIdAndDietianId(ClientId, DietianId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getDietListByClientId")]
        public IActionResult getDietList(int ClientId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                List<ClientDietList> sad = new List<ClientDietList>();
                sad = context.ClientDietList.OrderByDescending(x => x.Session).Where(c => c.ClientId == ClientId).ToList();
                sad = sad.DistinctBy(x => x.Session).ToList();
                if (sad != null)
                {
                    var result = new SuccessDataResult<List<ClientDietList>>(sad, Messages.Listed);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            
        }

        [HttpGet("getDietListBySessionAndClientId")]
        public IActionResult getDietListBySessionAndClientId(int ClientId,int Session)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                List<ClientDietList> sad = new List<ClientDietList>();
                sad = context.ClientDietList.Where(c=> c.ClientId==ClientId && c.Session == Session).ToList();
                foreach (var item in sad)
                {
                    item.FoodNames = getFoodNames(item);
                }
                if (sad != null)
                {
                    var result = new SuccessDataResult<List<ClientDietList>>(sad, Messages.Listed);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }

        }

        [HttpPost("add")]
        public IActionResult add(ClientDietList[] clientDietLists)
        {
            ClientDietList lastItem;
            using (DietManagerContext context = new DietManagerContext())
            {
                lastItem = context.ClientDietList.OrderByDescending(x => x.DietDate).LastOrDefault();

            }
            foreach (var clientDietList in clientDietLists)
            {
                if(lastItem != null)
                {
                    clientDietList.Session = lastItem.Session+1;
                }
                else { clientDietList.Session = 1; }
                
                var result = _clientDietListService.Add(clientDietList);
                
            }

                return Ok();
            
        }

        [HttpPost("delete")]
        public IActionResult Delete(ClientDietList clientDietList)
        {
            var result = _clientDietListService.Delete(clientDietList);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(ClientDietList clientDietList)
        {
            var result = _clientDietListService.Update(clientDietList);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private string[] getFoodNames(ClientDietList clientDietList)
        {
            string[] res = new string[clientDietList.DietInfo.Split(",").Length];
            var tempData = clientDietList.DietInfo.Split(",");
            using (DietManagerContext context = new DietManagerContext())
            {
                var foodlist = context.FoodList.ToList();
                for (int i = 0; i < clientDietList.DietInfo.Split(",").Length; i++)
                {
                   var tempFood = foodlist.FirstOrDefault(f=>f.Id+"" == tempData[i]);
                    if(tempFood != null)
                    {
                        res[i] = tempFood.FoodName;
                    }
                }

            }
            return res;
        }



    }
}
