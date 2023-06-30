using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Group2_Sem3_Accountant.Controllers
{
    [Route("api/advances")]
    [ApiController]
    public class AdvanceController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public AdvanceController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var advance = _context.Advances.ToList<Advance>();
            return Ok(advance);
        }

        [HttpGet]
        [Route ("get-by-id")]
        public IActionResult Get(int id)
        {
            var advance = _context.Advances.Where(a => a.Id == id).First();
            return Ok(advance);
        }

        [HttpPost]
        public IActionResult Create(Advance advance)
        {
            _context.Advances.Add(advance);
            _context.SaveChanges();
            return Created($"/get-by-id?id={advance.Id}", advance);
        }
       
        [HttpPut]
        public IActionResult Update(Advance advance)
        {
            _context.Advances.Update(advance);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var advance = _context.Advances.Find(id);
            _context.Advances.Remove(advance);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
