namespace PizzaShop.Service.Interfaces;

public interface ISendmailService
{
    Task SendMail(string toEmail, string? resetLink);
}
