using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// Что делать с трафиком после обработки(после выхода в сеть)
/// </summary>
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Класс нужен как DTO, абстрактность не требуется")]
public class OutboundDto
{
    /// <summary>
    /// Отправка данных в сеть в данном случае:
    /// Protocol = "freedom" - отправь данные в сеть,без прокси,тунелей,маски
    /// </summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; set; }
}