using HR.LeaveManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
