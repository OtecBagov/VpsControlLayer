using System.Text.Json;
using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// Точка входа в сервис
/// </summary>
public class XrayDto
{
    /// <summary>
    /// Откуда данные приходят
    /// </summary>
    [JsonPropertyName("inboundsDto")]
    public InboundDto[]? InboundsDto { get; set; }
    
    /// <summary>
    /// Откуда данные уходят
    /// </summary>
    [JsonPropertyName("outboundDto")]
    public OutboundDto[]? OutboundsDto { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; set; }
}