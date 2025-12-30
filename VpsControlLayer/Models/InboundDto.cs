using System.Text.Json;
using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// входная ручка
/// </summary>
public class InboundDto
{
    /// <summary>
    /// На каком порту сервис принимает соединение
    /// </summary>
    [JsonPropertyName("port")]
    public int? Port { get; set; }
    
    /// <summary>
    /// Какой формат данных ожидается для проксирования
    /// </summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; set; }
    
    /// <summary>
    /// Кто имеет право подключаться 
    /// </summary>
    [JsonPropertyName("settings")]
    public InboundSettingsDto? Settings { get; set; }
    
    /// <summary>
    /// как соединение торчит(маскировка,транспорт)
    /// </summary>
    [JsonPropertyName("streamSettings")]
    public StreamSettingsDto?  StreamSettings { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; set; }
}