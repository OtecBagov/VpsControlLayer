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
    /// айдишник подключаемого
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// режим соединения(как будут передаваться данные HTTP/1.1 ,HTTP/2.2  и т.д.)
    /// </summary>
    [JsonPropertyName("flow")]
    public string? Flow {get; set; }
}