using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.Context;
using Entities.Concrete;
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
    public class AppointmentController : Controller
    {
      
        [HttpGet("GetByClientAppointments")]
        public IActionResult GetByClientAppointments(int clientId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                 
                var result =  new SuccessDataResult<List<Appointment>>(context.Appointment.Where(a=>a.ClientId ==clientId).ToList(), Messages.Listed);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }

        [HttpGet("GetNextAppointments")]
        public IActionResult GetNextAppointments(int dietianId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                var result = new SuccessDataResult<List<Appointment>>(context.Appointment.Where(a => a.DietianId == dietianId && a.AppointmentDate>DateTime.Now).ToList(), Messages.Listed);
                var x = context.Appointment.Where(a => a.ClientId == dietianId).ToList();
                foreach (var item in x)
                {
                    var y = item.AppointmentDate > DateTime.Now;
                }
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult add(Appointment appointment)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                context.Appointment.Add(appointment);
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);
            }
        }

        [HttpPost("update")]
        public IActionResult update(Appointment appointment)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                var uptatedEntity = context.Entry(appointment);
                uptatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);

            }
        }

    }
}
