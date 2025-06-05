using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer.Services.Implementations;

public class TaskReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TaskReminderService> _logger;

    public TaskReminderService(IServiceProvider serviceProvider, ILogger<TaskReminderService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Temporary: Run after 5 seconds for testing
            await Task.Delay(5000, stoppingToken); // 5-second delay
            await SendReminderEmails();

            // Original: Run daily at 8 AM
            // var now = DateTime.Now;
            // var nextRun = now.Date.AddDays(1).AddHours(8); // Run at 8 AM
            // var delay = nextRun - now;
            // await Task.Delay(delay, stoppingToken);
            // await SendReminderEmails();
        }
    }

    private async Task SendReminderEmails()
    {
        using var scope = _serviceProvider.CreateScope();
        var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        var tasksDueTomorrow = await taskService.GetTasksDueTomorrow();
        foreach (var task in tasksDueTomorrow)
        {
            var success = await emailService.SendTaskReminderEmail(task.UserEmail, task.TaskTitle, task.DueDate);
            if (success)
            {
                // Mark task as reminder sent
                var taskItem = await dbContext.TaskItems
                    .FirstOrDefaultAsync(t => t.Title == task.TaskTitle && t.DueDate == task.DueDate && t.UserLogin.Email == task.UserEmail && !t.IsDelete);
                if (taskItem != null)
                {
                    taskItem.ReminderSent = true;
                    await dbContext.SaveChangesAsync();
                }
            }
            else
            {
                _logger.LogWarning("Failed to send reminder email for task {TaskTitle} to {UserEmail}", task.TaskTitle, task.UserEmail);
            }
        }
    }
}