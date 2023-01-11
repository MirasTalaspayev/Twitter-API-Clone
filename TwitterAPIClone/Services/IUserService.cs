using TwitterAPIClone.Models;

namespace TwitterAPIClone.Services;
public interface IUserService
{
    public void PostTweet(Tweet tweet);
    public IEnumerable<object> GetFeed(int userId);
    public void Follow(int userId, int userToFollowId);
    public void UnFollow(int userId, int userToUnFollowId);
}

