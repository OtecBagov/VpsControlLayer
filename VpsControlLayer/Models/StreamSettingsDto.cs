using System.Text.Json;
using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// Как будут данные дергатся в сети
/// </summary>
public class StreamSettingsDto
{
    /// <summary>
    /// Тип транспорта tcp и т.д.
    /// </summary>
    [JsonPropertyName("network")]
    public string? Network { get; set; }
    
    /// <summary>
    /// есть ли маска(маскировка) reality = HTTPS, параметры = RealitySettings
    /// </summary>
    [JsonPropertyName("security")]
    public string? Security { get; set; }
    
    [JsonPropertyName("realitySettings")]
    public RealitySettingsDto? RealitySettings { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; set; }
}