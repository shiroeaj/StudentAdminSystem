using MailKit.Net.Smtp;
using MimeKit;

namespace StudentAdminSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendRegistrationEmailAsync(string recipientEmail, string fullName)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["Email:SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = "Welcome to StudentAdminSystem";

            var body = $@"
                
Welcome {fullName}!

                

Your registration has been successful.


                

You can now login to your account and start shopping.


            ";

            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:SmtpServer"], int.Parse(_config["Email:SmtpPort"]));
            await smtp.AuthenticateAsync(_config["Email:SenderEmail"], _config["Email:SenderPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendOrderConfirmationEmailAsync(string recipientEmail, string orderId, decimal totalAmount)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["Email:SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = "Order Confirmation";

            var body = $@"
                
Order Confirmation

                

Your order has been confirmed!


                

Order ID: {orderId}


                

Total Amount: ${totalAmount}


            ";

            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:SmtpServer"], int.Parse(_config["Email:SmtpPort"]));
            await smtp.AuthenticateAsync(_config["Email:SenderEmail"], _config["Email:SenderPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendOrderCancelledEmailAsync(string recipientEmail, string orderId)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["Email:SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = "Order Cancelled";

            var body = $@"
                
Order Cancelled

                

Your order has been cancelled.


                

Order ID: {orderId}


            ";

            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:SmtpServer"], int.Parse(_config["Email:SmtpPort"]));
            await smtp.AuthenticateAsync(_config["Email:SenderEmail"], _config["Email:SenderPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendReceiptEmailAsync(string recipientEmail, string orderDetails)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["Email:SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(recipientEmail));
            email.Subject = "Order Receipt";

            var body = $@"
                
Order Receipt

                

{orderDetails}


            ";

            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:SmtpServer"], int.Parse(_config["Email:SmtpPort"]));
            await smtp.AuthenticateAsync(_config["Email:SenderEmail"], _config["Email:SenderPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}