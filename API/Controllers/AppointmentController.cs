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
        private int[] hours = {9,10,11,12,13,14,15,16,17,18 };
        private int[] minutes = { 00, 10, 20, 30, 40, 50 };
        
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
        [HttpGet("getUserNextAppointment")]
        public IActionResult asd(int clientId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                
                var AppointmentList =  context.Appointment.Where(a=>a.ClientId==clientId && a.AppointmentDate> DateTime.Now).OrderBy(a=>a.AppointmentDate).ToList();
                var nextAppointment = AppointmentList.FirstOrDefault();
                if (nextAppointment!=null)
                {
                    return Ok(nextAppointment);
                }
                else
                {
                    return BadRequest();
                }
                

            }
        }

        [HttpGet("GetTodayAppointments")]
        public IActionResult GetTodayAppointments(int dietianId,int day,int month,int year)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                var y = DateTime.Now.AddHours(8-(DateTime.Now.Hour)).AddDays(day-DateTime.Now.Day).AddMonths(month-DateTime.Now.Month).AddYears(year-DateTime.Now.Year);
                
                var temp = context.Appointment.Where(a => a.DietianId == dietianId && a.AppointmentDate > y && a.AppointmentDate < y.AddDays(1) && a.AppointmentType == "Measurement && Talk").ToList();
                foreach (var item in temp)
                {
                    var tempUser = context.Users.FirstOrDefault(u=>u.Id ==item.ClientId);
                    item.FullName = tempUser.FirstName + " " + tempUser.LastName;
                }
                
                var result = new SuccessDataResult<List<Appointment>>(temp, Messages.Listed);
                
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }

        [HttpGet("GetTodayAppointmentHours")]
        public IActionResult GetTodayAppointmentHours(int clientId,int day, int month, int year)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                List<string> avaibleHours = new List<string>();
                int dietianId = context.DietianClients.FirstOrDefault(u => u.ClientId == clientId).DietianId;
                var y = DateTime.Now.AddHours(8 - (DateTime.Now.Hour)).AddDays(day - DateTime.Now.Day).AddMonths(month - DateTime.Now.Month).AddYears(year - DateTime.Now.Year);

                var temp = context.Appointment.Where(a => a.DietianId == dietianId && a.AppointmentDate > y && a.AppointmentDate < y.AddDays(1) && a.AppointmentType == "Measurement && Talk").ToList();
                foreach (var item in hours)
                {
                    foreach (var item2 in minutes)
                    {
                        DateTime startDate = DateTime.Now.AddDays(day - DateTime.Now.Day).AddMonths(month - DateTime.Now.Month).AddYears(year - DateTime.Now.Year).AddHours(-(DateTime.Now.Hour)).AddMinutes(-(DateTime.Now.Minute)).AddSeconds(-DateTime.Now.Second);
                        DateTime endDate = DateTime.Now.AddDays(day - DateTime.Now.Day).AddMonths(month - DateTime.Now.Month).AddYears(year - DateTime.Now.Year).AddHours(-(DateTime.Now.Hour)).AddMinutes(-(DateTime.Now.Minute)).AddSeconds(-DateTime.Now.Second);
                        endDate = endDate.AddHours(item).AddMinutes(item2+9).AddSeconds(59);
                        startDate = startDate.AddHours(item).AddMinutes(item2);
                        var tempHM = temp.FirstOrDefault(f => f.AppointmentDate >= startDate && f.AppointmentDate <= endDate);
                        if (tempHM == null)
                        {
                            string tempMin = item2 == 0 ? "00" : item2+"";
                            avaibleHours.Add($"{item}:{tempMin}");
                        }
                    }
  
                }

                return Ok(avaibleHours);

                
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
