using Group2_Sem3_Accountant.Dtos;
using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Group2_Sem3_Accountant.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private Group2Sem3Context _context;
        public ExpenseController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fo = _context.Financeouts.ToList<Financeout>();
            return Ok(fo);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var fo = _context.Financeouts.Find(id);
            return Ok(fo);
        }

        [HttpGet]
        [Route("get-by-active")]
        public IActionResult GetActive()
        {
            try
            {
                var fo = _context.Financeouts
                .Where(f => f.Status == 1)
                .ToArray();
                return Ok(fo);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-remove")]
        public IActionResult GetRemove()
        {
            try
            {
                var fo = _context.Financeouts
                .Where(f => f.Status == 4)
                .ToArray();
                return Ok(fo);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-pending")]
        public IActionResult GetPending()
        {
            try
            {
                var fo = _context.Financeouts
                .Where(f => f.Status == 2)
                .ToArray();
                return Ok(fo);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-deactive")]
        public IActionResult GetDeActive()
        {
            try
            {
                var fo = _context.Financeouts
                .Where(f => f.Status == 0)
                .ToArray();
                return Ok(fo);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-canceling")]
        public IActionResult GetCanceling()
        {
            try
            {
                var fo = _context.Financeouts
                .Where(f => f.Status == 3)
                .ToArray();
                return Ok(fo);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(CreateExpense createExpense, [FromHeader] int userId)
        {
            var expense = new Entities.Financeout
            {
                Name = createExpense.Name,
                Date = createExpense.Date,
                Description = createExpense.Description,
                Document = createExpense.Document,
                UserCreateId = userId,
                TypefinanceoutId = createExpense.TypefinanceoutId,
                Amount = createExpense.Amount,
                CreatedAt = createExpense.CreatedAt,
            };
            _context.Financeouts.Add(expense);
            _context.SaveChanges();
            return Created($"/get-by-id?id={expense.Id}", expense);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateExpense updateExpense)
        {
            try
            {
                var expense = _context.Financeouts.Find(id);
                if (expense == null)
                    return NotFound("Khong co du lieu");
                if(expense.Status == 2)
                {
                    expense.Name = updateExpense.Name != null ? updateExpense.Name : expense.Name;
                    expense.Date = (DateTime)(updateExpense.Date != null ? updateExpense.Date : expense.Date);
                    expense.Description = updateExpense.Description != null ? updateExpense.Description : expense.Description;
                    expense.Document = updateExpense.Document != null ? updateExpense.Document : expense.Document;
                    expense.TypefinanceoutId = updateExpense.TypefinanceoutId != null ? updateExpense.TypefinanceoutId : expense.TypefinanceoutId;
                    expense.Amount = (Decimal)(updateExpense.Amount != null ? updateExpense.Amount : expense.Amount);
                    expense.UpdatedAt = updateExpense.UpdatedAt;
                    _context.SaveChanges();
                }
                else
                {
                    return NotFound("Không có dữ liệu về khoản thu này");
                }
                return Ok(expense);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Route("active")]
        public IActionResult Active(int id, [FromHeader] int userId)
        {
            try
            {
                var expense = _context.Financeouts.Find(id);
                var fund_salary = _context.Funds.Find(1);
                if (expense != null)
                {
                    expense.Status = 1;
                    expense.UserId = userId;
                    fund_salary.Amount = fund_salary.Amount - expense.Amount;
                    _context.SaveChanges();
                }
                else
                {
                    return NotFound("Không có dữ liệu về khoản thu này");
                }
                return Ok(expense);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("deactive")]
        public IActionResult DeActive(int id, [FromHeader] int userId)
        {
            try
            {
                var expense = _context.Financeouts.Find(id);
                if (expense != null)
                {
                    expense.Status = 0;
                    expense.UserId = userId;
                    _context.SaveChanges();
                }
                else
                {
                    return NotFound("Không có dữ liệu về khoản thu này");
                }
                return Ok(expense);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("remove")]
        public IActionResult Remove(int id, [FromHeader] int userId)
        {
            try
            {
                var fo = _context.Financeouts.Find(id);
                var fund_salary = _context.Funds.Find(1);
                fo.Status = 4;
                fund_salary.Amount = fund_salary.Amount - fo.Amount;
                fo.UserId = userId;
                _context.SaveChanges();
                return Ok($"Đã xóa {fo.Name} khỏi danh sách");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("canceling")]
        public IActionResult Canceling(int id)
        {
            try
            {
                var fo = _context.Financeouts.Find(id);
                fo.Status = 3;
                _context.SaveChanges();
                return Ok($"Đã xóa {fo.Name} khỏi danh sách");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var fo = _context.Financeouts.Find(id);
            if (fo == null)
                return NotFound("Khong co du lieu");
            if (fo.Status != 2)
                return BadRequest("Khong xoa duoc");
            _context.Financeouts.Remove(fo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
