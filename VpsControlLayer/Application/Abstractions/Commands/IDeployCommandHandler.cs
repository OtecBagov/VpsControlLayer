namespace VpsControlLayer.Application.Abstractions.Commands;

/// <summary>
/// Обработчик команды деплоя конфигурации на VPS
/// </summary>
public interface IDeployCommandHandler
{
    void Handle();
}