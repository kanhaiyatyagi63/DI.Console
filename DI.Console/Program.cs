// See https://aka.ms/new-console-template for more information
using DI.Console.Extensions;
using DI.Console.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

//How to create scope in console application
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services.ConfigureServices())
    .Build();

IConfiguration configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .AddEnvironmentVariables()
      .Build();

Console.WriteLine(configuration.GetValue<string>("AppSettings:Email"));

var consoleService = host.Services.GetRequiredServices<IConsoleService>();

consoleService.SendMessage("Welcome to the Team kanhaiya tyagi!");

await host.RunAsync();