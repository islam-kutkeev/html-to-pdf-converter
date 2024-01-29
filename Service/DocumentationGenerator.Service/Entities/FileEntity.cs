using System.ComponentModel.DataAnnotations.Schema;
using DocumentationGenerator.Service.Constants;

namespace DocumentationGenerator.Service.Entities;

[Table("FILE")]
public class FileEntity
{
    [Column("ID")]
    public Guid Id { get; set; }

    [Column("CREATE_AT")]
    public DateTime CreatedAt { get; set; }
    
    [Column("NAME")]
    public string? Name { get; set; }

    [Column("SAVED_PATH")]
    public string? SavedPath { get; set; }

    [Column("STATUS")]
    public FileStatus Status { get; set; }
}
