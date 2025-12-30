using VpsControlLayer.Application.Abstractions.Commands;

namespace VpsControlLayer.Application.Commands;

/// <summary>
/// Обработчик команды проверки статуса VPN сервера.
/// Реализует интерфейс <see cref="IStatusCommandHandler"/>.
/// 
/// <para>
/// <b>ЧТО ДЕЛАЕТ:</b><br/>
/// - Подключается к серверу по SSH<br/>
/// - Проверяет статус xray сервиса (systemctl status xray) TODO написать Enum состояния для статуса сервиса<br/>
/// - Показывает количество активных клиентов<br/>
/// - Показывает uptime сервера<br/>
/// - Проверяет доступность порта (ping)
/// </para>
/// 
/// <para>
/// <b>ЗАЧЕМ НУЖЕН:</b><br/>
/// Быстрая проверка работоспособности стороннего VPN сервера без ручного подключения по SSH.
/// Помогает мониторить состояние и вовремя обнаружить проблемы.
/// </para>
/// 
/// <para>
/// <b>ПРИМЕР ИСПОЛЬЗОВАНИЯ:</b><br/>
/// <c>vpnctl status</c>
/// </para>
/// 
/// <para>
/// <b>ПРИМЕР ВЫВОДА:</b><br/>
/// <code>
/// Статус VPN сервера
/// Сервер:     45.67.89.123  (ping: 25ms)
/// Xray:       Активен (uptime: 3d 12h)
/// Клиентов:   3 / 10
/// Трафик:     1.2 GB ↓ / 450 MB ↑
/// </code>
/// </para>
/// </summary>
/// <remarks>
/// здесь будут зависимости:<br/>
/// - ISshDeployer - для SSH подключения и выполнения команд
/// </remarks>
public class StatusCommandHandler : IStatusCommandHandler
{
    // TODO: добавить зависимости    
    
    /// <summary>
    /// Обрабатывает команду проверки статуса.
    /// Подключается к серверу и собирает информацию о его состоянии.
    /// </summary>
    public void Handle()
    {
        Console.WriteLine("Статус сервера:");
        Console.WriteLine("  Сервис: работает ");
        Console.WriteLine("  Клиентов: 0");
        Console.WriteLine("(заглушка)");
    }
}