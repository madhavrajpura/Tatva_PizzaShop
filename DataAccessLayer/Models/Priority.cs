using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Priority
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
