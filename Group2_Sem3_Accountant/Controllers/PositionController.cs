using Group2_Sem3_Accountant.Dtos;
using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/positions")]
    public class PositionController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public PositionController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var position = _context.Positions
               .ToList<Position>();
            return Ok(position);
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var position = _context.Positions.Find(id);
            if (position == null)
                return NotFound();
            return Ok(position);
        }

        [HttpPost]
        public IActionResult Create(Position position)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
            return Created($"/get-by-id?id={position.Id}", position);
        }

        [HttpPut]
        public IActionResult Update( Position position)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var position = _context.Positions.Find(id);
            if (position == null)
                return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

