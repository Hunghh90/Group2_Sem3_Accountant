using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Group2_Sem3_Accountant.Dtos;

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

                //var staff = (from s in _context.Staffs
                //            join p_t in _context.Positions on s.PositionId equals p_t.Id into p_tb
                //            from p in p_tb.DefaultIfEmpty()
                //            join d_t in _context.Departments on s.DepartmentId equals d_t.Id into d_tb
                //            from d in d_tb.DefaultIfEmpty()
                //            select new
                //            {
                //                Id = s.Id,
                //                Name = s.Name,
                //                Address = s.Address,
                //                PositionName = p.Name,
                //                DepartmentName = d.Name,
                //            })
                //            .ToList();

                var staff = _context.Staffs
                .Include(s => s.Position)
                .Include(s => s.Department)
                .ToArray();
                return Ok(staff);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            try
            {
                var staff = _context.Staffs
               .Where(s => s.Id == id)
               .Where(s => s.Status == 1)
               .Include(s => s.Position)
               .Include(s => s.Department)
               .FirstOrDefault();
                if (staff == null)
                    return NotFound("Nhân viên này không tồn tại hoặc đã nghỉ việc");
                return Ok(staff);
            } catch
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
                var staff = _context.Staffs
               .Where(s => s.Status == 0)
               .Include(s => s.Position)
               .Include(s => s.Department)
               .FirstOrDefault();
                return Ok(staff);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-by-active")]
        public IActionResult GetActive()
        {
            try
            {
                var staff = _context.Staffs
               .Where(s => s.Status == 1)
               .Include(s => s.Position)
               .Include(s => s.Department)
               .FirstOrDefault();
                return Ok(staff);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(CreateStaff createStaff)
        {
            var staff = new Entities.Staff
            {
                Name = createStaff.Name,
                Email = createStaff.Email,
                Address = createStaff.Address,
                Telephone = createStaff.Telephone,
                DepartmentId = createStaff.DepartmentId,
                PositionId = createStaff.PositionId,
                Birthday = createStaff.Birthday,
                JoinDate = createStaff.JoinDate,
                CreatedAt = createStaff.CreatedAt,

            };
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return Created($"/get-by-id?id={staff.Id}", staff);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateStaff updateStaff)
        {
            var staff = _context.Staffs
                .Where(s => s.Status == 1)
                .Where(s=> s.Id == id)
                .FirstOrDefault();
                if(staff != null)
            {
                staff.Name = updateStaff.Name != null ? updateStaff.Name : staff.Name;
                staff.Birthday = updateStaff.Birthday != null ? updateStaff.Birthday : staff.Birthday;
                staff.Address = updateStaff.Address != null ? updateStaff.Address : staff.Address;
                staff.Email = updateStaff.Email != null ? updateStaff.Email : staff.Email;
                staff.Telephone = updateStaff.Telephone != null ? updateStaff.Telephone : staff.Telephone;
                staff.PositionId = updateStaff.PositionId !=null ? updateStaff.PositionId.Value : staff.PositionId;
                staff.DepartmentId = updateStaff.DepartmentId !=null ? updateStaff.DepartmentId.Value : staff.DepartmentId;
                staff.JoinDate = updateStaff.JoinDate !=null ? updateStaff.JoinDate.Value : staff.JoinDate;
                staff.UpdatedAt = updateStaff.UpdatedAt.Value;
                _context.SaveChanges();
                return Ok("Cập nhật thành công thông tin nhân viên");
            } else
            {
                return NotFound("Nhân viên này không tồn tại hoặc đã nghỉ việc");
            }
            
           
        }

        [HttpPut]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            try
            {
                var staff = _context.Staffs
               .Where(s => s.Status == 1)
               .Where(s => s.Id == id)
               .FirstOrDefault();
                if (staff == null)
                    return NotFound("Nhân viên này không tồn tại hoặc đã nghỉ việc");
                staff.Status = 0;
                _context.SaveChanges();
                return Ok($"Đã xóa {staff.Name} khỏi danh sách nhân viên");
            } catch
            {
                return BadRequest();
            }
            
        }
    }
}
