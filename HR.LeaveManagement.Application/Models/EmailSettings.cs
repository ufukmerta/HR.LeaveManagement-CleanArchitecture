using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Models
{
    public class EmailSettings
    {
        public string ApiKey { get; set; } = null!;
        public string FromAddress { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
