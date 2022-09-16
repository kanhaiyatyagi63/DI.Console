using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DI.Core.Services.Abstractions;

public interface IEmailService
{
    IEmailService Init();
    IEmailService AddTo(IList<string> emails);
    IEmailService AddPriority(bool isUrgent);
    IEmailService AddCc(IList<string> emails);
    IEmailService AddBcc(IList<string> emails);
    IEmailService AddFrom(string email);
    IEmailService AddSubject(string subject);
    IEmailService AddBody(string content);
    IEmailService AddAlternateView(AlternateView alternateView);
    IEmailService AddAttachment(Attachment attachment);
    Task<bool> Send();
}
