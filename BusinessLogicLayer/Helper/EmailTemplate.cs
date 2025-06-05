namespace BusinessLogicLayer.Helper;

public static class EmailTemplate
{
    public static string TaskReminderEmail(string taskTitle, DateTime dueDate)
    {
        return $@"
            <html>
                <body>
                    <h2>Task Reminder: {taskTitle}</h2>
                    <p>Dear User,</p>
                    <p>This is a reminder that your task <strong>{taskTitle}</strong> is due on <strong>{dueDate:MMMM dd, yyyy}</strong>.</p>
                    <p>Please ensure you complete it on time. Log in to the Task Manager to view or update the task.</p>
                    <p><a href='http://yourappurl.com/Tasks'>Go to Task Manager</a></p>
                    <p>Best regards,<br/>Task Manager Team</p>
                </body>
            </html>";
    }
}