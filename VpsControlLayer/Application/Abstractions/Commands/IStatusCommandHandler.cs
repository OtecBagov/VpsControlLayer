namespace VpsControlLayer.Application.Abstractions.Commands;

/// <summary>
/// Обработчик команды проверки статуса VPN сервера
/// </summary>
public interface IStatusCommandHandler
{
    void Handle();
}