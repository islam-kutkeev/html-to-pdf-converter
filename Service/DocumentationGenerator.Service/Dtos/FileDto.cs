using System.Text.Json.Serialization;

namespace DocumentationGenerator.Service.Dtos;

public class FileDto
{
    /// <summary>
    /// Identifier of uploaded file
    /// </summary>
    /// <example>062a9dc3-7cb2-4e2a-b255-74ee8f974ebb</example>
    public string Id { get; set; }

    /// <summary>
    /// File creation date
    /// </summary>
    /// <example>01.01.2000 23:59:59</example>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// File original name
    /// </summary>
    /// <example>MyPage</example>
    public string? Name { get; set; }

    /// <summary>
    /// File current status
    /// </summary>
    /// <example>New</example>
    public string? Status { get; set; }

    /// <summary>
    /// File content
    /// </summary>
    [JsonIgnore]
    public byte[]? Content { get; set; }

    /// <summary>
    /// File content type
    /// </summary>
    [JsonIgnore]
    public string? ContentType { get; set; }
}
