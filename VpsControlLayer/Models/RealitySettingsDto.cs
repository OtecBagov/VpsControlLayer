using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VpsControlLayer.Models;

/// <summary>
/// Маскировка под прикладной протокол HTTPS
/// </summary>
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Класс нужен как DTO, абстрактность не требуется")]
public class RealitySettingsDto
{
    /// <summary>
    /// на какой ресурс мы конектимся под маской(сервис,сайт и т.д.)
    /// </summary>
    [JsonPropertyName("dest")]
    public string? Dest {get; set; }
    
    /// <summary>
    /// Домен который видит криптографический протокол TLS(ресурс который мы дергаем под маской)
    /// </summary>
    [JsonPropertyName("serverName")]
    public string[]? ServerName {get; set; }
    
    /// <summary>
    /// Секрет моего сервака,доказывает получателю ,что он законектился именно к моему серваку
    /// Нужен для шифрования соединения
    /// <returns>key который никода не уходит клиенту(подключаемому)</returns>>
    /// </summary>
    [JsonPropertyName("privateKey")]
    public string? PrivateKey {get; set; }
    
    /// <summary>
    /// Секрет встраиваемый в начало соединения(в заголовок) если его не будет при конекте сервер не ответит
    /// нужен для безопасности соединения,что-бы ни кто "левый" не могу постучатся к серваку
    /// </summary>
    [JsonPropertyName("shortIds")]
    public string[]? ShortIds { get; set; }
    
    /// <summary>
    /// Публичный ключ который уходит клиенту исходя из PrivateKey.
    /// PublicKey вычисляется с помощью секрета и отправляется клиенту
    /// </summary>
    [JsonPropertyName("publicKey")]
    public string[]? PublicKey {get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra {get; set; }
}