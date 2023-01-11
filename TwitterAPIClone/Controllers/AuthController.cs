using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAPIClone.Data;
using TwitterAPIClone.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TwitterAPIClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AppDbContext _dbContext;

        public AuthController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public ActionResult Register(UserRequest request)
        {
            if (_dbContext.UserSecrets.FirstOrDefault(u => u.Username == request.Username) is not null)
                return BadRequest("User with this username already exists");
            
            var user = new User()
            {
                Username = request.Username
            };
            var userSecrets = new UserSecrets()
            {
                Username = request.Username,
                Password = request.Password,
                UserId = user.Id,
                User = user
            };
            _dbContext.Users.Add(user);
            _dbContext.UserSecrets.Add(userSecrets);
            _dbContext.SaveChanges();

            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserRequest request)
        {
            var userSecrets = _dbContext.UserSecrets.FirstOrDefault(u => u.Username == request.Username);
            if (userSecrets is null)
            {
                return BadRequest("There is no such user");
            }
            if (userSecrets.Password != request.Password)
            {
                return BadRequest("Password is not correct");
            }
            var user = userSecrets.User;

            var claims = new List<Claim>()
            { 
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            HttpContext.Response.Cookies.Append("id", user.Id.ToString());
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties());
            return Ok();
        }
        [HttpPost("logout"), Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
