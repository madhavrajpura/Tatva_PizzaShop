using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; } = false;
    
    [Required]
    public bool IsDelete { get; set; } = false;

    [Required]
    [ForeignKey("UserLogin")]
    public int UserLoginId { get; set; }

    [Required]
    [ForeignKey("Priority")]
    public int PriorityId { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
        public bool ReminderSent { get; set; } // New property


    public virtual Category Category { get; set; } = null!;
    public virtual Priority Priority { get; set; } = null!;
    public virtual UserLogin UserLogin { get; set; } = null!;
}