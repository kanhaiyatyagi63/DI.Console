using DI.Console.Services.Abstractions;

namespace DI.Console.Services;

internal class ConsoleService : IConsoleService
{
    public void SendMessage(string message)
    {
        System.Console.WriteLine(message);
    }
}
