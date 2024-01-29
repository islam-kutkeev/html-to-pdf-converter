using DocumentationGenerator.Service.Dtos;

namespace DocumentationGenerator.Service.Services.FileManagerService;

public interface IFileManagerService
{
    /// <summary>
    /// Obtains all uploaded files
    /// </summary>
    /// <returns>List of <see cref="FileDto"/></returns>
    Task<List<FileDto>> GetAllFilesAsync();

    /// <summary>
    /// Saves new html file on server
    /// </summary>
    /// <param name="file">Uploaded file</param>
    /// <returns>Saved file information <see cref="FileDto"/></returns>
    Task<FileDto> SaveFileAsync(IFormFile file);

    /// <summary>
    /// Deletes all information about file and all files from server
    /// </summary>
    /// <param name="id">File's identifier</param>
    Task DeleteFileAsync(string id);

    /// <summary>
    /// Obtains file's content and metadata
    /// </summary>
    /// <param name="id">File's identifier</param>
    /// <param name="extension">File's extension</param>
    /// <returns>Metadata of file <see cref="FileDto?"/></returns>
    Task<FileDto?> ObtainFileAsync(string id, string extension);
}
