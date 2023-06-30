using Group2_Sem3_Accountant.Dtos;
using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public DepartmentController(Group2Sem3Context context)
        {
            _context = context;
        }   

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _context.Departments
               .ToList<Department>();
            return Ok(departments);
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var department = _context.Departments.Find(id);
            if(department == null) 
               return NotFound(); 
            return Ok(department);
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return Created($"/get-by-id?id={department.Id}", department);
        }

        [HttpPut]
        public IActionResult Update(Department department)
        { 
            _context.Departments.Update(department);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
                return NotFound();
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
