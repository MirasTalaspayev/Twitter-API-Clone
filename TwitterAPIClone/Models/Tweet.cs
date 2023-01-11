using System.ComponentModel.DataAnnotations;

namespace TwitterAPIClone.Models;
public class Tweet : BaseModel
{
    public string Text { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; }
}