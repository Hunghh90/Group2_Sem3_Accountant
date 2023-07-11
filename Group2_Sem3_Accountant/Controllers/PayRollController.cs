using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;
using Group2_Sem3_Accountant.Dtos;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/payroll")]
    public class PayRollController : ControllerBase
    {
        private Group2Sem3Context _context;
        public PayRollController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pr = _context.Payrolls.ToList<Payroll>();
            return Ok(pr);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult GetById(int id)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("Khong co du lieu");
            return Ok(pr);
        }

        [HttpGet]
        [Route("get-by-active")]
        public IActionResult GetByActive()
        {
            var pr = _context.Payrolls.Where(p => p.Status == 1).ToArray();
            if (pr.Length > 0)
                return Ok(pr);
            return NotFound("Khong co du lieu");
        }

        [HttpGet]
        [Route("get-by-pending")]
        public IActionResult GetByPending()
        {
            var pr = _context.Payrolls.Where(p => p.Status == 2).ToArray();
            if (pr.Length > 0)
                return Ok(pr);
            return NotFound("Khong co du lieu");
        }

        [HttpGet]
        [Route("get-by-deactive")]
        public IActionResult GetByDeActive()
        {
            var pr = _context.Payrolls.Where(p => p.Status == 0).ToArray();
            if (pr.Length > 0)
                return Ok(pr);
            return NotFound("Khong co du lieu");
        }

        [HttpGet]
        [Route("get-by-remove")]
        public IActionResult GetByRemove()
        {
            var pr = _context.Payrolls.Where(p => p.Status == 4).ToArray();
            if (pr.Length > 0)
                return Ok(pr);
            return NotFound("Khong co du lieu");
        }

        [HttpGet]
        [Route("get-by-canceling")]
        public IActionResult GetByCanceling()
        {
            var pr = _context.Payrolls.Where(p => p.Status == 3).ToArray();
            if (pr.Length > 0)
                return Ok(pr);
            return NotFound("Khong co du lieu");
        }

        [HttpPost]
        public IActionResult Create(CreatePayRoll createPayRoll, [FromHeader] int userId)
        {
            var staff = _context.Staffs.Find(createPayRoll.StaffId);
            var pos = _context.Positions.Find(staff.PositionId);
            var dep = _context.Departments.Find(staff.DepartmentId);
            var bonus = createPayRoll.Bonus / createPayRoll.ActualWorkday * createPayRoll.Workday;
            var insurance = staff.Basesalary / 100;
            var allowance = (dep.Allowance * pos.Coefficient)/ createPayRoll.ActualWorkday * createPayRoll.Workday;
            var actualWorkingDaySalary = ((staff.Basesalary / createPayRoll.ActualWorkday) * (createPayRoll.Workday + createPayRoll.PaidLeaver) * pos.Coefficient);
            var totalSalary = actualWorkingDaySalary + bonus - insurance - createPayRoll.UnionDues - createPayRoll.Reduce;
            var payroll = new Entities.Payroll
            {
                Name = createPayRoll.Name,
                Workday = createPayRoll.Workday,
                BaseSalary = (decimal)staff.Basesalary,
                PaidLeaver = createPayRoll.PaidLeaver,
                UnpaidLeaver = createPayRoll.UnpaidLeaver,
                TotalSalary = (decimal)totalSalary,
                ActualWorkingDaySalary = (decimal)actualWorkingDaySalary,
                Allowance = allowance,
                UserCreateId = userId,
                StaffId = createPayRoll.StaffId,
                Insurance = insurance,
                UnionDues = createPayRoll.UnionDues,
                Reduce = createPayRoll.Reduce,
                Bonus = bonus,
                CreatedAt = createPayRoll.CreatedAt,
            };
            _context.Payrolls.Add(payroll);
            _context.SaveChanges();
            return Created($"/get-by-id?id={payroll.Id}", payroll);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdatePayRoll updatePayRoll)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("Khong co du lieu");
            if (pr.Status != 2)
                return BadRequest("khong duoc sua");
           
            pr.Name = updatePayRoll.Name != null ? updatePayRoll.Name : pr.Name;
            pr.Workday = (int)(updatePayRoll.Workday != null ? updatePayRoll.Workday.Value : pr.Workday);
            pr.PaidLeaver = updatePayRoll.PaidLeaver !=null ? updatePayRoll.PaidLeaver.Value : pr.PaidLeaver;
            pr.UnpaidLeaver = updatePayRoll.UnpaidLeaver != null ? updatePayRoll.UnpaidLeaver.Value : pr.UnpaidLeaver;
            pr.UnionDues = updatePayRoll.UnionDues != null ? updatePayRoll.UnionDues.Value : pr.UnionDues;
            pr.Reduce = updatePayRoll.Reduce != null ? updatePayRoll.Reduce.Value : pr.Reduce;
            pr.Bonus = updatePayRoll.Bonus != null ? updatePayRoll.Bonus.Value : pr.Bonus;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("remove")]
        public IActionResult Remove(int id, [FromHeader] int userId)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("Khong co du lieu");
            pr.Status = 4;
            pr.UserId = userId;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("active")]
        public IActionResult Active(int id, [FromHeader] int userId)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("Khong co du lieu");
            pr.Status = 1;
            pr.UserId = userId;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("deactive")]
        public IActionResult DeActive(int id, [FromHeader] int userId)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("Khong co du lieu");
            pr.Status = 0;
            pr.UserId = userId;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Route("canceling")]
        public IActionResult Canceling(int id, [FromHeader] int userId)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("Khong co du lieu");
            pr.Status = 3;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pr = _context.Payrolls.Find(id);
            if (pr == null)
                return NotFound("khong co du lieu");
            if (pr.Status != 2)
                return NotFound("Khong xoa duoc");
            _context.Payrolls.Remove(pr);
            _context.SaveChanges();
            return Ok($"Xoa {pr.Name} thanh cong"); 
        }
    }
}
