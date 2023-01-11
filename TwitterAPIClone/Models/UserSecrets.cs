namespace TwitterAPIClone.Models;
public class UserSecrets : BaseModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public User? User { get; set; }
}

