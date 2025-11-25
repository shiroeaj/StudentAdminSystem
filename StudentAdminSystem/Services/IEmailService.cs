namespace StudentAdminSystem.Services
{
    public interface IEmailService
    {
        Task SendRegistrationEmailAsync(string recipientEmail, string fullName);
        Task SendOrderConfirmationEmailAsync(string recipientEmail, string orderId, decimal totalAmount);
        Task SendOrderCancelledEmailAsync(string recipientEmail, string orderId);
        Task SendReceiptEmailAsync(string recipientEmail, string orderDetails);
    }
}