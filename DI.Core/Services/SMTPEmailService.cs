using DI.Core.Models.Base;
using DI.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace DI.Core.Services;

public class SMTPEmailService : IEmailService, IDisposable
{
    private readonly ILogger<SMTPEmailService> _logger;
    private readonly IEmailSetting _emailSetting;
    private MailMessage _message = null;
    public SMTPEmailService(ILogger<SMTPEmailService> logger, IEmailSetting emailSetting)
    {
        _logger = logger;
        _emailSetting = emailSetting;
    }

    public IEmailService Init()
    {
        _message = new MailMessage();
        return this;
    }

    public IEmailService AddAlternateView(AlternateView alternateView)
    {
        return this;
    }

    public IEmailService AddAttachment(Attachment attachment)
    {
        if (attachment != null)
        {
            _message.Attachments.Add(attachment);
        }

        return this;
    }

    public IEmailService AddBcc(IList<string> emails)
    {
        foreach (var email in emails)
        {
            _message.Bcc.Add(new MailAddress(email));
        }
        return this;
    }

    public IEmailService AddBody(string content)
    {
        _message.IsBodyHtml = true; //to make message body as html  
        _message.Body = content;
        return this;
    }

    public IEmailService AddCc(IList<string> emails)
    {
        foreach (var email in emails)
        {
            _message.CC.Add(new MailAddress(email));
        }
        return this;
    }

    public IEmailService AddFrom(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            _message.From = new MailAddress(email);
        }
        return this;
    }

    public IEmailService AddPriority(bool isUrgent)
    {
        return this;
    }

    public IEmailService AddSubject(string subject)
    {
        _message.Subject = subject;
        return this;
    }

    public IEmailService AddTo(IList<string> emails)
    {
        foreach (var email in emails)
        {
            _message.To.Add(new MailAddress(email));
        }
        return this;
    }

    public Task<bool> Send()
    {
        if (_message.From == null)
        {
            _message.From = new MailAddress(_emailSetting.SenderEmail, _emailSetting.SenderName);
        }

        SmtpClient smtp = new SmtpClient
        {
            Port = _emailSetting.Port,
            Host = _emailSetting.Host
        };

        if (!string.IsNullOrEmpty(_emailSetting.UserName))
        {
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_emailSetting.UserName, _emailSetting.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        try
        {
            _logger.LogDebug($"Send email using {_emailSetting.Host} and {_emailSetting.Port}");
            smtp.Send(_message);
            _logger.LogDebug($"Email sent");
            return Task.FromResult(true);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured during sending mail");
            return Task.FromResult(false);
        }
    }

    public void Dispose()
    {
        _message = null;
    }
}
