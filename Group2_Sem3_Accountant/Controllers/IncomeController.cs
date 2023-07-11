using Group2_Sem3_Accountant.Dtos;
using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/incomes")]
    public class IncomeController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public IncomeController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fi = _context.Financeins.ToList<Financein>();
            return Ok(fi);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var fi = _context.Financeins.Find(id);
            return Ok(fi);
        }

        [HttpGet]
        [Route("get-by-active")]
        public IActionResult GetActive()
        {
            try
            {
                var fi = _context.Financeins
                .Where(f => f.Status == 1)
                .ToArray();
                return Ok(fi);
            } catch
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
                var fi = _context.Financeins
                .Where(f => f.Status == 3)
                .ToArray();
                return Ok(fi);
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
                var fi = _context.Financeins
                .Where(f => f.Status == 2)
                .ToArray();
                return Ok(fi);
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
                var fi = _context.Financeins
                .Where(f => f.Status == 4)
                .ToArray();
                return Ok(fi);
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
                var fi = _context.Financeins
                .Where(f => f.Status == 0)
                .ToArray();
                return Ok(fi);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(CreateIncome createIncome, [FromHeader] int userId)
        {
            var income = new Entities.Financein
            {
                Name = createIncome.Name,
                Date = createIncome.Date,
                Description = createIncome.Description,
                Document = createIncome.Document,
                UserCreateId = userId,
                TypefinanceinId = createIncome.TypefinanceinId,
                Amount = createIncome.Amount,
                CreatedAt = createIncome.CreatedAt,
            };
            _context.Financeins.Add(income);
            _context.SaveChanges();
            return Created($"/get-by-id?id={income.Id}", income);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateIncome updateIncome)
        {
            try
            {
                var income = _context.Financeins.Find(id);
                if (income == null)
                    return NotFound("Khong co du lieu");
                if(income.Status == 2)
                {
                    income.Name = updateIncome.Name != null ? updateIncome.Name : income.Name;
                    income.Date = (DateTime)(updateIncome.Date != null ? updateIncome.Date : income.Date);
                    income.Description = updateIncome.Description != null ? updateIncome.Description : income.Description;
                    income.Document = updateIncome.Document != null ? updateIncome.Document : income.Document;
                    income.TypefinanceinId = updateIncome.TypefinanceinId != null ? updateIncome.TypefinanceinId : income.TypefinanceinId;
                    income.Amount = (Decimal)(updateIncome.Amount != null ? updateIncome.Amount : income.Amount);
                    income.UpdatedAt = updateIncome.UpdatedAt;
                    _context.SaveChanges();
                }
                else
                {
                    return NotFound("Khong duoc sua");
                }
                return Ok(income);
            } catch
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
                var income = _context.Financeins.Find(id);
                var fund_salary = _context.Funds.Find(1);
                if (income != null)
                {
                    income.Status = 1;
                    income.UserId = userId;
                    fund_salary.Amount = fund_salary.Amount + income.Amount;
                    _context.SaveChanges();
                }
                else
                {
                    return NotFound("Không có dữ liệu về khoản thu này");
                }
                return Ok(income);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("remove")]
        public IActionResult Remove (int id, [FromHeader] int userId)
        {
            try
            {
                var fi = _context.Financeins.Find(id);
                var fund_salary = _context.Funds.Find(1);
                fi.Status = 4;
                fi.UserId = userId;
                fund_salary.Amount = fund_salary.Amount - fi.Amount;
                _context.SaveChanges();
                return Ok($"Đã xóa {fi.Name} khỏi danh sách");
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("deactive")]
        public IActionResult Deactive(int id, [FromHeader] int userId)
        {
            try
            {
                var fi = _context.Financeins.Find(id);
                fi.Status = 0;
                fi.UserId = userId;
                _context.SaveChanges();
                return Ok();
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
                var fi = _context.Financeins.Find(id);
                fi.Status = 3;
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var fi = _context.Financeins.Find(id);
            if (fi != null)
                return NotFound("Khong co du lieu");
            if(fi.Status != 2 )
                return BadRequest("Khong xoa duoc");
            _context.Financeins.Remove(fi);
            _context.SaveChanges();
            return Ok($"Xoa {fi.Name} thanh cong");
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search( string? name, int? limit, int? page, [FromHeader] int userId)
        {
            limit = limit != null ? limit : 10;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            var user = _context.Users.Where(u => u.Name == name).FirstOrDefault();
            var products = _context.Financeins
                .Where(p => p.UserId == user.Id)
                .Skip(offset).Take((int)limit).ToArray();
            return Ok(products);
        }
    }
}
