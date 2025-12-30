using VpsControlLayer.Application.Abstractions.Commands;

namespace VpsControlLayer.Application.Commands;

/// <summary>
/// Обработчик команды ротации ключей(Секретов) TODO вынести секреты в секретницу.
/// Реализует интерфейс <see cref="IRotateKeysCommandHandler"/>.
/// 
/// <para>
/// <b>ЧТО ДЕЛАЕТ:</b><br/>
/// - Генерирует новую пару x25519 ключей (Private/Public)<br/>
/// - Обновляет конфиг сервера новыми ключами<br/>
/// - Сохраняет старые ключи для отката (резервная копия)<br/>
/// - Генерирует обновленные клиентские конфиги с новым Public Key
/// </para>
/// 
/// <para>
/// <b>ЗАЧЕМ НУЖЕН:</b><br/>
/// Периодическая смена ключей TODO вынести в параметр.
/// </para>
/// 
/// <para>
/// <b>ПРИМЕР ИСПОЛЬЗОВАНИЯ:</b><br/>
/// <c>vpnctl rotate-keys</c><br/>
/// После этого нужно:<br/>
/// 1. <c>vpnctl deploy</c> - применить на сервере<br/>
/// 2. Раздать новые клиентские конфиги всем пользователям
/// </para>
/// </summary>
/// <remarks>
/// <b>ВАЖНО:</b> После ротации ключей старые клиентские конфиги перестанут работать!<br/>
/// Все клиенты должны получить обновленные конфиги с новым Public Key.
/// <br/><br/>
///  здесь будут зависимости:<br/>
/// - IKeyGenerator - для генерации новых ключей<br/>
/// - IConfigGenerator - для обновления конфига
/// </remarks>
public class RotateKeysCommandHandler : IRotateKeysCommandHandler
{
    // TODO: добавить зависимости    
    
    /// <summary>
    /// Обрабатывает команду ротации ключей.
    /// Генерирует новые ключи и обновляет конфигурацию.
    /// </summary>
    public void Handle()
    {
        Console.WriteLine("Ротация ключей...");
        Console.WriteLine("Новые ключи сгенерированы! (пока заглушка)");
    }
}