using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using VpsControlLayer.Application.Abstractions.Commands;

namespace VpsControlLayer.CLI;

/// <summary>
/// РЕГА КОМАНД CLI
/// 
/// ЧТО ЭТО?
/// ЗАНОСИТ в пайп лайн всех команд нашего CLI приложения.
/// Здесь мы создаем команды, которые пользователь может вызвать из консоли.
/// 
/// КАК ЭТО РАБОТАЕТ?
/// 1. Program.cs вызывает CreateRootCommand()
/// 2. Мы создаем все команды (init, deploy, status, rotate-keys и т.д.)
/// 3. Возвращаем их в виде "корневой команды"
/// 4. System.CommandLine парсит ввод пользователя и вызывает нужную команду
/// </summary>
public static class CommandRegistry
{
    /// <summary>
    /// СОЗДАЕТ КОРНЕВУЮ КОМАНДУ
    /// 
    /// ЧТО ДЕЛАЕТ:
    /// Создает главную команду CLI, к которой прикрепляются все остальные подкоманды.
    /// 
    /// ПАРАМЕТРЫ:
    /// serviceProvider - "контейнер" с нашими зависимостями (handlers).
    ///                   Нужен чтобы достать нужный handler когда пользователь вызовет команду.
    /// 
    /// ВОЗВРАЩАЕТ:
    /// RootCommand - объект, который содержит все наши команды (init, deploy, status, rotate-keys)
    /// 
    /// ПРИМЕР ИСПОЛЬЗОВАНИЯ:
    /// dotnet run -- init --server 1.2.3.4 --domain google.com
    ///               ^^^^
    ///               это команда "init" которую мы здесь регистрируем
    /// </summary>
    public static RootCommand CreateRootCommand(IServiceProvider serviceProvider)
    {
        // Создаем корневую команду с описанием (покажется при --help)
        var rootCommand = new RootCommand("VPS Control Layer - CLI инструмент для управления VPN сервером");

        // Добавляем к ней все наши команды
        // Каждая команда создается отдельным методом ниже
        rootCommand.AddCommand(CreateInitCommand(serviceProvider));      // vpnctl init
        rootCommand.AddCommand(CreateDeployCommand(serviceProvider));    // vpnctl deploy
        rootCommand.AddCommand(CreateStatusCommand(serviceProvider));    // vpnctl status
        rootCommand.AddCommand(CreateRotateKeysCommand(serviceProvider));// vpnctl rotate-keys

        return rootCommand; // Отдаем готовую команду с подкомандами в Program.cs
    }

    /// <summary>
    /// КОМАНДА: init
    /// 
    /// ЧТО ДЕЛАЕТ:
    /// Инициализирует конфигурацию VPN сервера - генерирует ключи, создает конфиг и т.д..
    /// 
    /// ЗАЧЕМ НУЖНА:
    /// Это первая команда которую запускает пользователь при настройке нового сервера.
    /// 
    /// КАК ВЫЗВАТЬ:
    /// dotnet run -- init --server 192.168.1.1 --domain google.com --port 443 --user root
    ///               ^^^^   ^^^^^^^^ обязательный  ^^^^^^^^ обязательный  ^^^^^^ опциональный
    /// 
    /// ЧТО ПРОИСХОДИТ ВНУТРИ:
    /// 1. Парсим параметры из командной строки (--server, --domain и т.д.)
    /// 2. Достаем из контейнера handler (InitCommandHandler)
    /// 3. Вызываем handler.Handle() передавая все параметры
    /// 4. Handler делает всю работу (генерация ключей, создание конфига)
    /// </summary>
    private static Command CreateInitCommand(IServiceProvider serviceProvider)
    {
        // ШАГ 1: Определяем какие параметры нужны команде
        
        // --server (ОБЯЗАТЕЛЬНЫЙ) - IP адрес VPS сервера
        var serverOption = new Option<string>("--server", "IP адрес VPS сервера") { IsRequired = true };
        
        // --domain (ОБЯЗАТЕЛЬНЫЙ) - домен для маскировки трафика
        var domainOption = new Option<string>("--domain", "Домен для маскировки (например, google.com)") { IsRequired = true };
        
        // --port (ОПЦИОНАЛЬНЫЙ) - порт, по умолчанию 443
        var portOption = new Option<int>("--port", () => 443, "Порт для подключения");
        
        // --user (ОПЦИОНАЛЬНЫЙ) - SSH пользователь, по умолчанию root TODO переконфигурировать в парамтр
        var userOption = new Option<string>("--user", () => "root", "SSH пользователь");

        // ШАГ 2: Создаем саму команду и говорим какие параметры она принимает
        var command = new Command("init", "Инициализация конфигурации для нового VPN сервера")
        {
            serverOption,  // добавляем параметр --server
            domainOption,  // добавляем параметр --domain
            portOption,    // добавляем параметр --port
            userOption     // добавляем параметр --user
        };

        // ШАГ 3: Указываем ЧТО ДЕЛАТЬ когда пользователь вызовет эту команду
        command.SetHandler((server, domain, port, user) =>
        {
            // Достаем handler из контейнера зависимостей (DI)
            var handler = serviceProvider.GetRequiredService<IInitCommandHandler>();
            
            // Вызываем метод Handle() передавая все параметры которые ввел пользователь
            handler.Handle(server, domain, port, user);
            
        }, serverOption, domainOption, portOption, userOption); // указываем откуда брать значения параметров

        return command; // Возвращаем готовую команду
    }

    /// <summary>
    /// КОМАНДА: deploy
    /// 
    /// ЧТО ДЕЛАЕТ:
    /// Загружает сгенерированную конфигурацию на VPS сервер и перезапускает сервис.
    /// 
    /// ЗАЧЕМ НУЖНА:
    /// После того как init создал конфиг - его нужно залить на сервер. Этим и занимается deploy.
    /// 
    /// КАК ВЫЗВАТЬ:
    /// dotnet run -- deploy
    ///               ^^^^^^ нет параметров - всё берется из сохраненного конфига
    /// 
    /// ЧТО ПРОИСХОДИТ:
    /// 1. Берем готовый конфиг из ~/.vpnctl/server-config.json
    /// 2. Подключаемся к серверу по SSH
    /// 3. Загружаем конфиг
    /// 4. Перезапускаем xray сервис
    /// </summary>
    private static Command CreateDeployCommand(IServiceProvider serviceProvider)
    {
        // Создаем команду без параметров (она всё берет из файла конфига)
        var command = new Command("deploy", "Деплой конфигурации на VPS сервер");
        
        // Указываем что делать при вызове
        command.SetHandler(() =>
        {
            // Достаем handler из DI контейнера
            var handler = serviceProvider.GetRequiredService<IDeployCommandHandler>();
            
            // Вызываем метод Handle() - он сделает всю работу
            handler.Handle();
        });
        
        return command;
    }

    /// <summary>
    /// КОМАНДА: status
    /// 
    /// ЧТО ДЕЛАЕТ:
    /// Проверяет статус внешнего VPN сервера - работает ли сервис, сколько клиентов подключено.
    /// 
    /// ЗАЧЕМ НУЖНА:
    /// Чтобы проверить что всё работает после деплоя или просто мониторить состояние.
    /// 
    /// КАК ВЫЗВАТЬ:
    /// dotnet run -- status
    ///               ^^^^^^ без параметров
    /// 
    /// ЧТО ПОКАЖЕТ:
    /// - Статус xray сервиса (работает/не работает)
    /// - Количество подключенных клиентов
    /// - Uptime сервера
    /// </summary>
    private static Command CreateStatusCommand(IServiceProvider serviceProvider)
    {
        var command = new Command("status", "Проверка статуса VPN сервера");
        
        command.SetHandler(() =>
        {
            // Достаем handler из контейнера
            var handler = serviceProvider.GetRequiredService<IStatusCommandHandler>();
            
            // Запрашиваем статус
            handler.Handle();
        });
        
        return command;
    }

    /// <summary>
    /// КОМАНДА: rotate-keys
    /// 
    /// ЧТО ДЕЛАЕТ:
    /// Генерирует новые ключи безопасности (x25519) для сервера.
    /// 
    /// ЗАЧЕМ НУЖНА:
    /// Периодическая ротация ключей.
    /// Например раз в месяц можно менять ключи.
    /// 
    /// КАК ВЫЗВАТЬ:
    /// dotnet run -- rotate-keys
    ///               ^^^^^^^^^^^
    /// 
    /// ЧТО ПРОИСХОДИТ:
    /// 1. Генерируются новые x25519 ключи
    /// 2. Обновляется конфиг
    /// 3. Нужно будет сделать deploy чтобы применить на сервере
    /// 4. Все клиенты получат новые конфиги с новым публичным ключом
    /// </summary>
    private static Command CreateRotateKeysCommand(IServiceProvider serviceProvider)
    {
        var command = new Command("rotate-keys", "Ротация ключей безопасности");
        
        command.SetHandler(() =>
        {
            // Достаем handler
            var handler = serviceProvider.GetRequiredService<IRotateKeysCommandHandler>();
            
            // Генерируем новые ключи
            handler.Handle();
        });
        
        return command;
    }
}