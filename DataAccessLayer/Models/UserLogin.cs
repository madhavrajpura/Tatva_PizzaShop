using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class UserLogin
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();

}