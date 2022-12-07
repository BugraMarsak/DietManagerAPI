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
    public class ClientMeasurementController : Controller
    {
      
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                 
                var result =  new SuccessDataResult<List<MeasurementResult>>(context.MeasurementResults.ToList(), Messages.Listed);
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
                var MData = context.MeasurementResults.Where(c => c.ClientId == userId).ToList();
                var result = new SuccessDataResult<List<MeasurementResult>>(MData, Messages.Listed);

                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }
        [HttpGet("getbyIdLastMeas")]
        public IActionResult getbyIdLastMeas(int userId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var MData = context.MeasurementResults.OrderBy(x=>x.MeasurementTime).LastOrDefault(c => c.ClientId == userId);
                var result = new SuccessDataResult<MeasurementResult>(MData, Messages.Listed);

                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }

        

        [HttpPost("add")]
        public IActionResult add(MeasurementResult measurementResult)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                context.MeasurementResults.Add(measurementResult);
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);
            }
        }

        [HttpPost("update")] 
        public IActionResult update(MeasurementResult measurementResult)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                    var uptatedEntity = context.Entry(measurementResult);
                    uptatedEntity.State = EntityState.Modified;
                    context.SaveChanges();
                    var result = new SuccessResult(Messages.added);
                    return Ok(result);

            }
        }


        [HttpGet("getClientDefData")]
        public IActionResult getClientDefData(int userId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var MData = context.ClientDefaultData.OrderBy(x => x.Id).LastOrDefault(c => c.ClientId == userId);
                var result = new SuccessDataResult<ClientDefaultData>(MData, Messages.Listed);

                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }


        [HttpPost("adddef")]
        public IActionResult adddef(ClientDefaultData clientDefaultData)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                context.ClientDefaultData.Add(clientDefaultData);
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);
            }
        }

        [HttpPost("updatedef")]
        public IActionResult updatedef(ClientDefaultData clientDefaultData)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                var uptatedEntity = context.Entry(clientDefaultData);
                uptatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);

            }
        }



    }
}
