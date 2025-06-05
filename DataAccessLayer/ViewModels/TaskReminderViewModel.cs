namespace DataAccessLayer.ViewModels;

public class TaskReminderViewModel
{
    public string TaskTitle { get; set; }
    public DateTime DueDate { get; set; }
    public string UserEmail { get; set; }
}
