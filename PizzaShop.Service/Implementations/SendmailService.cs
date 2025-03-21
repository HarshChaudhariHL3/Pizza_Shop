using System.Net;
using System.Net.Mail;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class SendmailService : ISendmailService
{
    private async Task SendMail(string toEmail, string? resetLink)
    {
        try
        {
            var fromEmail = "test.dotnet@etatvasoft.com";
            var fromPassword = "P}N^{z-]7Ilp";
            var smtpHost = "mail.etatvasoft.com";
            var smtpPort = 587;

            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(toEmail));
                message.From = new MailAddress(fromEmail);
                message.Subject = "Password Reset Link";
                message.Body = $@"
            <div >
                <header style='background-color: blue; padding: 10px; text-align: center;display: flex; justify-content: center; align-items: center;'>
                    <img src='~/../images/pizzashop_logo.png' alt='logo' style='width: 100px; height: 100px;'>
                    <h1>Pizzashop</h1>  
                </header>
                <main style='padding: 10px;'>
                    <p>Pizza Shop</p>
                    
                    <p>Please Click the <a href='{resetLink}' style='color: blue;'><u>link</u></a> below to reset your password</p>
                    <br>
                    <p>If you encounter any issues, or have any question, please do not hesitate to contact our support team.</p>
                    <br>
                    <p><span style='color: orange'>Important Note:</span> For security reasons, this link will expire in 24 hours. if you did not
                    request a password reset, please ignore this email or support our contact team immediatly</p>
                </main>

            ";
                message.IsBodyHtml = true;


                using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email Sending Fail: {ex.Message}");
        }
    }

    Task ISendmailService.SendMail(string toEmail, string? resetLink)
    {
        return SendMail(toEmail, resetLink);
    }

}
