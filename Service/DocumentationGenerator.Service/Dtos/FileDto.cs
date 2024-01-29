using System.Text.Json.Serialization;

namespace DocumentationGenerator.Service.Dtos;

public class FileDto
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }

    [JsonIgnore]
    public byte[]? Content { get; set; }

    [JsonIgnore]
    public string? ContentType { get; set; }
}
