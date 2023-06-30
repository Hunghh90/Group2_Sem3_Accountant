using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/typefinanceouts")]
    public class TypeFinanceOutController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public TypeFinanceOutController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tfo = _context.Typefinanceouts.ToList<Typefinanceout>();
            return Ok(tfo);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var tfo = _context.Typefinanceouts.Find(id);
            return Ok(tfo);
        }

        [HttpPost]
        public IActionResult Create(Typefinanceout financeout)
        {
            _context.Typefinanceouts.Add(financeout);
            _context.SaveChanges();
            return Created($"/get-by-id?id={financeout.Id}", financeout);
        }

        [HttpPut]
        public IActionResult Update(Typefinanceout financeout)
        {
            _context.Typefinanceouts.Update(financeout);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var tfo = _context.Typefinanceouts.Find(id);
            _context.Typefinanceouts.Remove(tfo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
