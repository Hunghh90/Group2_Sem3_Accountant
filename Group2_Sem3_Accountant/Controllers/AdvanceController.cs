using Group2_Sem3_Accountant.Dtos;
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
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var advance = _context.Advances.Find(id);
            if (advance == null)
                return NotFound("khong co du lieu");
            return Ok(advance);
        }


        [HttpGet]
        [Route("get-by-active")]
        public IActionResult GetByActive()
        {
            var advance = _context.Advances.Where(a => a.Status == 1).ToArray();
            if (advance.Length > 1)
                return NotFound("Khong co du lieu");
            return Ok(advance);
        }


        [HttpGet]
        [Route("get-by-deactive")]
        public IActionResult GetByDeActive()
        {
            var advance = _context.Advances.Where(a => a.Status == 0).ToArray();
            if (advance.Length > 1)
                return NotFound("Khong co du lieu");
            return Ok(advance);
        }

        [HttpGet]
        [Route("get-by-pending")]
        public IActionResult GetByPending()
        {
            var advance = _context.Advances.Where(a => a.Status == 2).ToArray();
            if (advance.Length > 1)
                return NotFound("Khong co du lieu");
            return Ok(advance);
        }


        [HttpGet]
        [Route("get-by-remove")]
        public IActionResult GetByRemove()
        {
            var advance = _context.Advances.Where(a => a.Status == 4).ToArray();
            if (advance.Length > 1)
                return NotFound("Khong co du lieu");
            return Ok(advance);
        }

        [HttpGet]
        [Route("get-by-canceling")]
        public IActionResult GetByCanceling()
        {
            var advance = _context.Advances.Where(a => a.Status == 3).ToArray();
            if (advance.Length > 1)
                return NotFound("Khong co du lieu");
            return Ok(advance);
        }

        [HttpPost]
        public IActionResult Create(CreateAdvance createAdvance, [FromHeader]int UserId)
        {
            var advance = new Entities.Advance
            {
                DateOfAdvances = createAdvance.DateOfAdvances,
                Amount = createAdvance.Amount,
                Description = createAdvance.Description,
                StaffId = createAdvance.StaffId,
                CreatedAt = createAdvance.CreatedAt,
                UserCreateId = UserId,
            };
            _context.Advances.Add(advance);
            _context.SaveChanges();
            return Created($"/get-by-id?id={advance.Id}", advance);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateAdvance updateAdvance)
        {
            var advance = _context.Advances.Find(id);
            if(advance == null)
                return NotFound("Khong co du lieu");
            if (advance.Status != 2)
                return BadRequest("Khong duoc sua doi");
            advance.UpdatedAt = updateAdvance.UpdatedAt;
            advance.DateOfAdvances = updateAdvance.DateOfAdvances != null ? updateAdvance.DateOfAdvances.Value : advance.DateOfAdvances;
            advance.Amount = updateAdvance.Amount != null ? updateAdvance.Amount.Value : advance.Amount;
            advance.Description = updateAdvance.Description != null ? updateAdvance.Description : advance.Description;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("active")]
        public IActionResult Active(int id, [FromHeader] int userId)
        {
            var advance = _context.Advances.Find(id);
            var fund_salary = _context.Funds.Find(1);
            if (advance == null)
                return NotFound("Khong co du lieu");
            advance.Status = 1;
            advance.UserId = userId;
            fund_salary.Amount = fund_salary.Amount - advance.Amount;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("deactive")]
        public IActionResult DeActive(int id, [FromHeader] int userId)
        {
            var advance = _context.Advances.Find(id);
            if (advance == null)
                return NotFound("Khong co du lieu");
            advance.Status = 0;
            advance.UserId = userId;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("remove")]
        public IActionResult Remove(int id, [FromHeader] int userId)
        {
            var advance = _context.Advances.Find(id);
            var fund_salary = _context.Funds.Find(1);
            if (advance == null)
                return NotFound("Khong co du lieu");
            advance.Status = 4;
            fund_salary.Amount = fund_salary.Amount + advance.Amount;
            advance.UpdatedAt = DateTime.UtcNow;
            advance.UserId = userId;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("canceling")]
        public IActionResult Canceling(int id)
        {
            var advance = _context.Advances.Find(id);
            if (advance == null)
                return NotFound("Khong co du lieu");
            advance.Status = 3;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var advance = _context.Advances.Find(id);
            if (advance == null)
                return NotFound("Khong co du lieu");
            if (advance.Status != 2)
                return BadRequest("Khong xoa duoc");
            _context.Advances.Remove(advance);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
