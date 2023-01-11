using Microsoft.EntityFrameworkCore;
using System.Linq;
using TwitterAPIClone.Data;
using TwitterAPIClone.Models;

namespace TwitterAPIClone.Services;
public class UserService : IUserService
{
    private IBaseService<User> _userService;
    private AppDbContext _dbContext;
    public UserService(AppDbContext dbContext, IBaseService<User> userService)
    {
        _userService = userService;
        _dbContext = dbContext;
    }
    public void Follow(int userId, int userToFollowId)
    {
        if (userId == userToFollowId)
        {
            return;
        }

        var user = _userService.Get(userId);
        var userToFollow = _userService.Get(userToFollowId);
        Console.WriteLine(user.Username);
        Console.WriteLine(userToFollow.Username);

        user.Following.Add(userToFollow);
        userToFollow.Followers.Add(user);

        _userService.Update(user);
        _userService.Update(userToFollow);
    }

    public IEnumerable<object> GetFeed(int userId)
    {
        var user = _userService.Get(userId);
        Console.WriteLine(user.Following.Count);
        foreach (var tweets in user.Following.Select(u => u.Tweets))
        {
            foreach (var tweet in tweets)
            {
                
            }
            yield return tweets.Select(u => new { u.Id, u.Text, u.UserId });
        }
    }

    public void PostTweet(Tweet tweet)
    {
        var user = tweet.User;
        user.Tweets.Add(tweet);
        _dbContext.SaveChanges();
    }

    public void UnFollow(int userId, int userToUnFollowId)
    {
        if (userId == userToUnFollowId)
        {
            return;
        }
        var user = _userService.Get(userId);
        var userToUnFollow = _userService.Get(userToUnFollowId);

        user.Following.Remove(userToUnFollow);
        userToUnFollow.Followers.Remove(user);

        _dbContext.SaveChanges();
    }
}

