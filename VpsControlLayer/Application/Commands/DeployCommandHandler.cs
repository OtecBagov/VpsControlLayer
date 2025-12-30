using VpsControlLayer.Application.Abstractions.Commands;

namespace VpsControlLayer.Application.Commands;

/// <summary>
/// Обработчик команды деплоя конфигурации на VPS.
/// Реализует интерфейс <see cref="IDeployCommandHandler"/>.
/// 
/// <para>
/// <b>ЧТО ДЕЛАЕТ:</b><br/>
/// - Читает конфиг из ~/.vpnctl/server-config.json<br/>
/// - Подключается к серверу по SSH<br/>
/// - Загружает конфиг в /usr/local/etc/xray/config.json TODO переконфигурировать на сервер нашу конфигурацию,а не ту что у нас в конфиге<br/>
/// - Перезапускает сервис xray (systemctl restart xray)
/// </para>
/// 
/// <para>
/// <b>ЗАЧЕМ НУЖЕН:</b><br/>
/// После генерации конфига командой <c>init</c> нужно залить его на сервер.
/// Эта команда автоматизирует этот процесс.
/// </para>
/// 
/// <para>
/// <b>ПРИМЕР ИСПОЛЬЗОВАНИЯ:</b><br/>
/// <c>vpnctl deploy</c>
/// </para>
/// </summary>
/// <remarks>
///здесь будут зависимости:<br/>
/// - ISshDeployer - для работы с SSH<br/>
/// - IConfigGenerator - для чтения конфига
/// </remarks>
public class DeployCommandHandler : IDeployCommandHandler
{
    // TODO: добавить зависимости    
    
    /// <summary>
    /// Обрабатывает команду деплоя.
    /// Загружает конфигурацию на сервер и перезапускает сервис.
    /// </summary>
    public void Handle()
    {
        Console.WriteLine("Деплой на сервер...");
        Console.WriteLine("Конфигурация загружена! (заглушка)");
    }
}