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
        IWebHostEnvironment env;
        public UserController(IUserService userService,IWebHostEnvironment env)
        {
            _userService = userService;
            this.env = env;
        }

        [HttpGet("test")]
        public IActionResult GetAll(User user)
        {
            var result = _userService.GetClaims(user);
            
            return Ok(result);
           
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int UserId)
        {
            var result = _userService.GetById(UserId);
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

        [HttpGet("setDietatian")] 
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

        [HttpGet, DisableRequestSizeLimit]
        public IActionResult GetPdf(int UserId)
        {


            string route = Path.Combine(env.WebRootPath, $"{UserId}_Files");
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }
            var filesInOrder = new DirectoryInfo(route).GetFiles()  //Dosyada bulunan dosyalarin isimlerini degisme tarihlerine gore aliyor.
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList();
            var filename = $"{filesInOrder.FirstOrDefault()}";



            string fileRoute = Path.Combine(route, filename);

            byte[] pdfData = System.IO.File.ReadAllBytes(fileRoute);

            return File(pdfData, "application/pdf");
        }


        [HttpGet("getByName"), DisableRequestSizeLimit]
        public IActionResult GetByPdfName(int UserId, string pdfName)
        {


            string route = Path.Combine(env.WebRootPath, $"{UserId}_Files");
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }
            var filesInOrder = new DirectoryInfo(route).GetFiles()  //Dosyada bulunan dosyalarin isimlerini degisme tarihlerine gore aliyor.
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList();
            var filename = $"{filesInOrder.FirstOrDefault(f => f == pdfName)}";
            if (filename == "")
            {
                return BadRequest();
            }

            string fileRoute = Path.Combine(route, filename);

            byte[] pdfData = System.IO.File.ReadAllBytes(fileRoute);

            return File(pdfData, "application/pdf");
        }

        [HttpGet("getLatestPdf"), DisableRequestSizeLimit]
        public IActionResult getLatestPdf(int UserId)
        {

            string route = Path.Combine(env.WebRootPath, $"{UserId}_Files");
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }
            var filesInOrder = new DirectoryInfo(route).GetFiles()  //Dosyada bulunan dosyalarin isimlerini degisme tarihlerine gore aliyor.
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList();
            var filename = $"{filesInOrder.FirstOrDefault()}";
            if (filename=="")
            {
                return BadRequest();
            }

            string fileRoute = Path.Combine(route, filename);

            byte[] pdfData = System.IO.File.ReadAllBytes(fileRoute);

            return File(pdfData, "application/pdf");
        }

        [HttpPost("SavePdf"), DisableRequestSizeLimit]
        public async Task<IActionResult> PostPdf([FromForm] IFormFile file, int UserId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {



                string route = Path.Combine(env.WebRootPath, $"{UserId}_Files");
                if (!Directory.Exists(route))
                {
                    Directory.CreateDirectory(route);
                }
                var filesInOrder = new DirectoryInfo(route).GetFiles()
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList();
                var lastFile = filesInOrder.FirstOrDefault();
                if (lastFile != null)
                {
                    lastFile = lastFile.Substring(lastFile.LastIndexOf("_") + 1);
                }
                var filename = $"{UserId}_{DateTime.Now.ToString("s").Replace("T","-").Replace("-","_").Replace(":","_")}";
                
                string fileRoute = Path.Combine(route, filename);
                using (var stream = new FileStream(fileRoute, FileMode.Create))
                {
                    await file.OpenReadStream().CopyToAsync(stream);
                }

                return Ok();

            }
        }

        [HttpGet("getFileNames"), DisableRequestSizeLimit]
        public IActionResult GetFileNames(int UserId)
        {


            string route = Path.Combine(env.WebRootPath, $"{UserId}_Files");
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }
            var filesInOrder = new DirectoryInfo(route).GetFiles()  //Dosyada bulunan dosyalarin isimlerini degisme tarihlerine gore aliyor.
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList();

            return Ok(filesInOrder);
        }



    }
}
