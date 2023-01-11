using System.ComponentModel.DataAnnotations;

namespace TwitterAPIClone.Models
{
    public class User : BaseModel
    {        
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = Constants.USER_ROLE;
        public ICollection<Tweet> Tweets { get; set; } = new List<Tweet>();
        public ICollection<User> Following { get; set; } = new List<User>();
        public ICollection<User> Followers { get; set; } = new List<User>();

    }
    public class Constants
    {
        public const string USER_ROLE = "user";
        public const string ADMIN_ROLE = "admin";
    }
}