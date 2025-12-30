namespace VpsControlLayer.Application.Abstractions.Commands;

/// <summary>
/// Обработчик команды инициализации VPN сервера
/// </summary>
public interface IInitCommandHandler
{
    void Handle(string server, string domain, int port, string user);
}