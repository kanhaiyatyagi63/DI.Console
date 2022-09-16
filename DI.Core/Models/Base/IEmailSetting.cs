using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Core.Models.Base;

public interface IEmailSetting
{
    string ApiKey { get; set; }
    string SenderName { get; set; }
    string SenderEmail { get; set; }
    int MaxRetryCount { get; set; }

    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
