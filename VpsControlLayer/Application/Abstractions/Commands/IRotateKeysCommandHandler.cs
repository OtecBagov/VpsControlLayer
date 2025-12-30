namespace VpsControlLayer.Application.Abstractions.Commands;

/// <summary>
/// Обработчик команды ротации ключей безопасности
/// </summary>
public interface IRotateKeysCommandHandler
{
    void Handle();
}