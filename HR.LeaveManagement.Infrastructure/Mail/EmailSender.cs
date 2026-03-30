using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Infrastructure.Mail
{
    public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;
        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            // Response can be 200, 201, 202, 204 for success and 400, 401, 403, 406, 429, 500 for failure
            // based on documentation: https://www.twilio.com/docs/sendgrid/api-reference/how-to-use-the-sendgrid-v3-api/responses#http-status-codes
            // IsSuccessStatusCode will return true between 200 and 299, so it will cover all success status codes
            return response.IsSuccessStatusCode;
        }
    }
}
