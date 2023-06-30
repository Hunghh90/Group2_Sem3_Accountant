using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group2_Sem3_Accountant.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public UserController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _context.Users.ToList<User>();
            return Ok(user);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.Find(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create (User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Created($"/get-by-id?id={user.Id}",user);
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
