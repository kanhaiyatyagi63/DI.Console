using DI.Core.Models.Base;
using DI.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace DI.Core.Services;

public class SendGridService : IEmailService
{
    private readonly ILogger<SendGridService> _logger;
    private readonly IEmailSetting _sendGridSetting;
    private SendGridMessage _sendGridMessage = null;

    public SendGridService(ILogger<SendGridService> logger, IEmailSetting sendGridSetting)
    {
        _logger = logger;
        _sendGridSetting = sendGridSetting;
    }

    public IEmailService Init()
    {
        _sendGridMessage = new SendGridMessage();
        return this;
    }

    public IEmailService AddBcc(IList<string> emails)
    {
        if (emails.Any())
        {
            _sendGridMessage.AddBccs(emails.Select(email => new EmailAddress(email)).ToList());
        }
        return this;
    }

    public IEmailService AddBody(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentNullException(nameof(content));
        }

        _sendGridMessage.AddContent(MimeType.Html, content);
        return this;
    }

    public IEmailService AddCc(IList<string> emails)
    {
        if (emails.Any())
        {
            _sendGridMessage.AddCcs(emails.Select(email => new EmailAddress(email)).ToList());
        }
        return this;
    }

    public IEmailService AddFrom(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            _sendGridMessage.SetFrom(new EmailAddress(email));
        }

        return this;
    }

    public IEmailService AddSubject(string subject)
    {
        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentNullException(nameof(subject));
        }

        _sendGridMessage.SetSubject(subject);
        return this;
    }

    public IEmailService AddTo(IList<string> emails)
    {
        if (emails.Any())
        {
            _sendGridMessage.AddTos(emails.Select(email => new EmailAddress(email)).ToList());
        }
        return this;
    }

    public IEmailService AddPriority(bool isUrgent)
    {
        if (isUrgent)
        {
            _sendGridMessage.AddHeader("Priority", "Urgent");
            _sendGridMessage.AddHeader("Importance", "high");
        }

        return this;
    }

    public IEmailService AddAlternateView(AlternateView alternateView)
    {
        if (alternateView != null)
        {
            var stream = alternateView.ContentStream;
            using var reader = new StreamReader(stream);
            _sendGridMessage.AddContent(alternateView.ContentType.ToString(), reader.ReadToEnd());
        }
        return this;
    }

    public IEmailService AddAttachment(System.Net.Mail.Attachment attachment)
    {
        if (attachment != null)
        {
            using var stream = new MemoryStream();
            try
            {
                attachment.ContentStream.CopyTo(stream);
                _sendGridMessage.AddAttachment(new SendGrid.Helpers.Mail.Attachment()
                {
                    Disposition = "attachment",
                    Type = attachment.ContentType.MediaType,
                    Filename = attachment.Name,
                    ContentId = attachment.ContentId,
                    Content = Convert.ToBase64String(stream.ToArray())
                });
            }
            finally
            {
                stream.Close();
            }
        }
        return this;
    }

    public async Task<bool> Send()
    {
        if (_sendGridMessage == null)
        {
            throw new ArgumentNullException("Invoke Init() method");
        }

        //If API Key is not defined, set status as false
        if (string.IsNullOrWhiteSpace(_sendGridSetting.Password))
        {
            return false;
        }

        if (_sendGridMessage.From == null || string.IsNullOrEmpty(_sendGridMessage.From.Email))
        {
            _sendGridMessage.SetFrom(_sendGridSetting.SenderEmail, _sendGridSetting.SenderName);
        }

        try
        {
            SendGridClient sendGridClient = new SendGridClient(_sendGridSetting.Password);
            var response = await sendGridClient.SendEmailAsync(_sendGridMessage).ConfigureAwait(true);
            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occured while sending email");
        }

        return false;
    }
}
}
