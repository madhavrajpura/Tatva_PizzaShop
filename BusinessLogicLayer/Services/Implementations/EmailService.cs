using System.Net.Mail;
     using System.Net;
     using Microsoft.Extensions.Configuration;
     using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Services.Interfaces;

namespace BusinessLogicLayer.Services.Implementations;

     public class EmailService : IEmailService
     {
         private readonly IConfiguration _configuration;

         public EmailService(IConfiguration configuration)
         {
             _configuration = configuration;
         }

         public async Task<bool> SendTaskReminderEmail(string recipientEmail, string taskTitle, DateTime dueDate)
         {
             try
             {
                 var senderEmail = new MailAddress(_configuration["smtp:SenderEmail"], _configuration["smtp:SenderName"]);
                 var receiverEmail = new MailAddress(recipientEmail);
                 var password = _configuration["smtp:SenderPassword"];
                 var username = _configuration.GetValue<string>("smtp:Username") ?? _configuration["smtp:SenderEmail"];
                 var subject = $"Task Reminder: {taskTitle}";
                 var body = EmailTemplate.TaskReminderEmail(taskTitle, dueDate);

                 var smtp = new SmtpClient
                 {
                     Host = _configuration["smtp:Host"],
                     Port = int.Parse(_configuration["smtp:Port"]),
                     EnableSsl = bool.Parse(_configuration["smtp:EnableSsl"]),
                     DeliveryMethod = SmtpDeliveryMethod.Network,
                     UseDefaultCredentials = bool.Parse(_configuration["smtp:UseDefaultCredentials"]),
                     Credentials = new NetworkCredential(username, password)
                 };

                 using (var message = new MailMessage(senderEmail, receiverEmail)
                 {
                     Subject = subject,
                     Body = body,
                     IsBodyHtml = true
                 })
                 {
                     await smtp.SendMailAsync(message);
                 }

                 return true;
             }
             catch (Exception ex)
             {
                 // Log the exception for debugging
                 Console.WriteLine($"Email sending failed: {ex.Message}");
                 return false;
             }
         }
     }