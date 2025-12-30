using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// Кто подключается
/// </summary>
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Класс нужен как DTO, абстрактность не требуется")]
public class ClientDto
{
    /// <summary>
    /// айдишник подключаемого (UUID формат, например: "b7a78e3d-4d3f-4e5e-9b1a-2c3d4e5f6a7b")
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    /// <summary>
    /// режим соединения(как будут передаваться данные HTTP/1.1 ,HTTP/2.2  и т.д.)
    /// </summary>
    [JsonPropertyName("flow")]
    public string? Flow {get; set; }
}