using Microsoft.AspNetCore.Mvc;
using Entities.Concrete;
using DataAccess.Concrete.Context;
using Core.Constants;
using Core.Utilities.Results;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        [HttpPost("add")]
        public IActionResult add(Notes notes)
        {
            using (DietManagerContext context = new DietManagerContext())
            {
                notes.NoteTime = DateTime.Now;
                context.Notes.Add(notes);
                context.SaveChanges();
                var result = new SuccessResult(Messages.added);
                return Ok(result);
            }
        }

        [HttpGet("GetByIds")]
        public IActionResult GetNotes(int clientId,int dietianId)
        {
            using (DietManagerContext context = new DietManagerContext())
            {

                var result = new SuccessDataResult<List<Notes>>(context.Notes.Where(a => a.ClientId == clientId && a.DietianId == dietianId).OrderByDescending(n=>n.NoteTime).ToList(), Messages.Listed);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }
    }
}
