using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// определяет правила доступа
/// </summary>
public class InboundSettingsDto
{
    /// <summary>
    /// определяет список разрешенных подключаемых клиентов
    /// </summary>
    [JsonPropertyName("clients")]
    public ClientDto[]? Clients { get; set; }
    
    /// <summary>
    /// Расшифровка протокола
    /// </summary>
    [JsonPropertyName("decryption")]
    public string? Decryption { get; set; }
}