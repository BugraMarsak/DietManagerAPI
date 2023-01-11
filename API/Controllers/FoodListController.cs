using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodListController : Controller
    {
        IFoodListService _foodListService;

        public FoodListController(IFoodListService foodListService)
        {
            _foodListService = foodListService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _foodListService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int FoodId)
        {
            var result = _foodListService.GetById(FoodId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetAllWithoutAllergens")]
        public IActionResult GetAllWithoutAllergens(int UserId)
        {
            var result = _foodListService.GetAllWithoutAllergens(UserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
