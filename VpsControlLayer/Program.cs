using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using VpsControlLayer.Application;
using VpsControlLayer.CLI;

namespace VpsControlLayer;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        // Настройка DI контейнера
        var services = new ServiceCollection();
        services.AddApplication();
        var serviceProvider = services.BuildServiceProvider();

        // Создание и запуск CLI
        var rootCommand = CommandRegistry.CreateRootCommand(serviceProvider);
        return await rootCommand.InvokeAsync(args);
    }
}