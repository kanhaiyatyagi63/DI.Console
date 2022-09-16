using DI.Core.Models.SendGrid;
using DI.Core.Services;
using DI.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSendGridService(this IServiceCollection services, Action<SGCredentials> action)
    {
        SGCredentials sGCredentials = null;

        services.RegisterServicesForSendGrid();
        return services;
    }

    private static IServiceCollection RegisterServicesForSendGrid(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}
