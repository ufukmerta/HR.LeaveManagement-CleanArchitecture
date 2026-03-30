using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Models
{
    public class Email
    {
        public string To { get; set; } = null!;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

    }
}
