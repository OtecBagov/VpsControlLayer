using VpsControlLayer.Application.Abstractions.Commands;

namespace VpsControlLayer.Application.Commands;

/// <summary>
/// Обработчик команды инициализации VPN сервера.
/// Реализует интерфейс <see cref="IInitCommandHandler"/>.
/// 
/// <para>
/// <b>ЧТО ДЕЛАЕТ:</b><br/>
/// - Генерирует x25519 ключи для Reality протокола<br/>
/// - Создает UUID для клиента<br/>
/// - Генерирует короткие ID (shortIds)<br/>
/// - Формирует серверный конфиг Xray<br/>
/// - Сохраняет конфиг в файл ~/.vpnctl/server-config.json TODO: сделать параметр обязательным,вынести конфигурацию из PATH в параметр И ПЕРЕИМЕНОВАТЬ НА *ConfigPath*
/// </para>
/// 
/// <para>
/// <b>ЗАЧЕМ НУЖЕН:</b><br/>
/// Это настройка сервера - создание конфигурации для сервера.
/// Без этого шага невозможно настроить сервер.
/// </para>
/// 
/// <para>
/// <b>ПРИМЕР ИСПОЛЬЗОВАНИЯ:</b><br/>
/// <c>vpnctl init --server 192.168.1.1 --domain google.com</c>
/// </para>
/// </summary>
/// <remarks>
/// здесь будут зависимости:<br/>
/// - IConfigGenerator - для генерации конфига<br/>
/// - IKeyGenerator - для генерации ключей
/// </remarks>
public class InitCommandHandler : IInitCommandHandler
{
    // TODO: добавить зависимости через конструктор    
    
    /// <summary>
    /// Обрабатывает команду инициализации.
    /// </summary>
    /// <param name="server">IP адрес VPS сервера</param>
    /// <param name="domain">Домен для маскировки трафика (например, google.com)</param>
    /// <param name="port">Порт для подключения (по умолчанию 443)</param>
    /// <param name="user">SSH пользователь для деплоя (по умолчанию root TODO: сделать параметр обязательным И ПЕРЕИМЕНОВАТЬ НА *UserName*)</param>
    public void Handle(string server, string domain, int port, string user)
    {
        Console.WriteLine(" Инициализация VPN сервера...");
        Console.WriteLine($"  Сервер: {server}");
        Console.WriteLine($"  Домен маски: {domain}");
        Console.WriteLine($"  Порт: {port}");
        Console.WriteLine($"  SSH пользователь: {user}");
        Console.WriteLine(" Конфигурация создана! (заглушка)");
    }
}