using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAPIClone.Models;
using TwitterAPIClone.Services;

namespace TwitterAPIClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = Constants.ADMIN_ROLE)]
    public class AdminPanelController : ControllerBase
    {
        private IBaseService<User> _userService;
        public AdminPanelController(IBaseService<User> userService)
        {
            _userService = userService;
        }
        [HttpGet("get-users")]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetAll();
        }
        [HttpGet("get-users/{id}")]
        public User GetUser(int id)
        {
            return _userService.Get(id);
        }
    }
}
