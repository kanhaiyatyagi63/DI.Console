using DI.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DI.Core.Services;

public class EmailService : IEmailService
{
	public EmailService()
	{

	}

	public IEmailService AddAlternateView(AlternateView alternateView)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddAttachment(Attachment attachment)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddBcc(IList<string> emails)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddBody(string content)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddCc(IList<string> emails)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddFrom(string email)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddPriority(bool isUrgent)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddSubject(string subject)
	{
		throw new NotImplementedException();
	}

	public IEmailService AddTo(IList<string> emails)
	{
		throw new NotImplementedException();
	}

	public IEmailService Init()
	{
		throw new NotImplementedException();
	}

	public Task<bool> Send()
	{
		throw new NotImplementedException();
	}
}
