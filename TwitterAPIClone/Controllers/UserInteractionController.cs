using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TwitterAPIClone.Models;
using TwitterAPIClone.Services;

namespace TwitterAPIClone.Controllers;
[Route("api/[controller]")]
[ApiController, Authorize]
public class UserInteractionController : ControllerBase
{
    private IUserService _userService;
    private IBaseService<User> _baseService;

    public UserInteractionController(IUserService userService, IBaseService<User> baseService)
    {
        _userService = userService;
        _baseService = baseService;
    }
    [HttpGet("feed")]
    public IEnumerable<object> GetFeed()
    {
        int id = GetId();
        return _userService.GetFeed(id);
    }
    [HttpPost("post-tweet")]
    public void PostTweet([FromBody] Tweet tweet)
    {
        int id = GetId();
        tweet.User = _baseService.Get(id);
        tweet.UserId = id;
        _userService.PostTweet(tweet);
    }
    [HttpPut("follow/{id}")]
    public void Follow(int id)
    {
        int userId = GetId();
        _userService.Follow(userId, id);
    }
    [HttpPut("unfollow/{id}")]
    public void UnFollow(int id)
    {
        int userId = GetId();
        _userService.UnFollow(userId, id);
    }
    private int GetId()
    {
        int id = Convert.ToInt32(HttpContext.Request.Cookies["id"]);
        return id;
    }
}
