namespace BusinessLogicLayer.Services.Interfaces;

public interface IEmailService
{
        Task<bool> SendTaskReminderEmail(string recipientEmail, string taskTitle, DateTime dueDate);

}
