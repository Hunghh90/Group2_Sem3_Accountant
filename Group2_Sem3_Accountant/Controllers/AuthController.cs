using Group2_Sem3_Accountant.Dtos;
using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        private readonly IConfiguration _config;
        public AuthController(Group2Sem3Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserRegister user)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var u = new Entities.User { 
                Email = user.Email, 
                Name = user.Name, 
                Birthday = user.Birthday, 
                Address = user.Address, 
                Telephone = user.Telephone,
                Role = user.Role,
                Permission = user.Permission,
                Password = hashed 
            };
            _context.Users.Add(u);
            _context.SaveChanges();
            return Ok(new UserData { Name = user.Name, Email = user.Email, Token = GenerateJWT(u) });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLogin userLogin)
        {
            var user = _context.Users.Where(u => u.Email.Equals(userLogin.Email))
                .First();
            if (user == null)
                return Unauthorized();
            bool verified = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);
            if (!verified)
                return Unauthorized();

            return Ok(new UserData { Name = user.Name, Email = user.Email, Token = GenerateJWT(user) });
        }

        private String GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signatureKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("Permission",user.Permission)
            };
            var token = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signatureKey
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
