using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route ("api/typefinanceins")]
    public class TypeFinanceInController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public TypeFinanceInController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index() 
        {
            var tfi = _context.Typefinanceins.ToList<Typefinancein>();
            return Ok(tfi);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var tfi = _context.Typefinanceins.Find(id);
            return Ok(tfi);
        }

        [HttpPost]
        public IActionResult Create(Typefinancein typefinancein)
        {
            _context.Typefinanceins.Add(typefinancein);
            _context.SaveChanges();
            return Created($"/get-by-id?id={typefinancein.Id}", typefinancein);
        }

        [HttpPut]
        public IActionResult Update(Typefinancein typefinancein)
        {
            _context.Typefinanceins.Update(typefinancein);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var tfi = _context.Typefinanceins.Find(id);
            _context.Typefinanceins.Remove(tfi);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
