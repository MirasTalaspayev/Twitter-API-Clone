using System.ComponentModel.DataAnnotations;

namespace TwitterAPIClone.Models;
public class BaseModel
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}

