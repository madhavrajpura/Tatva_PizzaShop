using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class TaskItemViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
    public string Title { get; set; }

    [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public int PendingCount { get; set; }
    public int CompletedCount { get; set; }
    public int PriorityId { get; set; }
    public int CategoryId { get; set; }
    public string PriorityName { get; set; }
    public string CategoryName { get; set; }
        public bool ReminderSent { get; set; } // New property

    

}
