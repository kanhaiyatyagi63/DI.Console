using DI.Core.Models.Base;
using DI.Core.Models.SendGrid;
using DI.Core.Services;
using DI.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSendGridService(this IServiceCollection services, Action<IEmailSetting> configure)
    {
        IEmailSetting emailSetting = default(IEmailSetting);

        configure(emailSetting);
        // need to add condition whic one you want to user
        services.RegisterServicesForSendGrid(emailSetting);

        services.RegisterServicesForSmtp(emailSetting);
        return services;
    }

    private static IServiceCollection RegisterServicesForSendGrid(this IServiceCollection services, IEmailSetting emailSetting)
    {
        services.AddScoped<IEmailService>(factory =>
        {
            return new SendGridService(factory.GetRequiredService<ILogger<SendGridService>>(), emailSetting);
        });
        return services;
    }

    private static IServiceCollection RegisterServicesForSmtp(this IServiceCollection services, IEmailSetting emailSetting)
    {
        services.AddScoped<IEmailService>(factory =>
        {
            return new SMTPEmailService(factory.GetRequiredService<ILogger<SMTPEmailService>>(), emailSetting);
        });
        return services;
    }
}
