using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
