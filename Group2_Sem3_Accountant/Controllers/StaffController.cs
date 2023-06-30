using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Group2_Sem3_Accountant.Controllers
{
    [Route("api/staffs")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public StaffController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var staff = _context.Staffs.Include(s => s.Department).Include(s => s.Position).ToArray();
                return Ok(staff);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var staff = _context.Staffs.Where(s => s.Id == id).Include(s => s.Position).Include(s => s.Department).ToArray();
            if (staff.Length == 0)
                return NotFound();
            return Ok(staff[0]);
        }

        [HttpPost]
        public IActionResult Create (Staff staff)
        {
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return Created($"/get-by-id?id={staff.Id}", staff);
        }

        [HttpPut]
        public IActionResult Update ( Staff staff)
        {
            _context.Staffs.Update(staff);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete (int id)
        {

            var staff = _context.Staffs.Find(id);
            if (staff == null)
                return NotFound();
            _context.Staffs.Remove(staff);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
