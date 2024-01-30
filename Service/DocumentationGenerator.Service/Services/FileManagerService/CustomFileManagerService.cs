using AutoMapper;
using DocumentationGenerator.Service.Constants;
using DocumentationGenerator.Service.Dtos;
using DocumentationGenerator.Service.Entities;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DocumentationGenerator.Service.Services.FileManagerService;

public class CustomFileManagerService : IFileManagerService
{
    private ILogger<CustomFileManagerService> _logger;
    private readonly DatabaseContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;


    public CustomFileManagerService(ILogger<CustomFileManagerService> logger, DatabaseContext dbContext, IConfiguration configuration, IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<List<FileDto>> GetAllFilesAsync()
    {
        var files = await _dbContext.Files.ToListAsync();
        return _mapper.Map<List<FileDto>>(files);
    }

    public async Task<FileDto> SaveFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            _logger.LogDebug("Provided file was empty");
            throw new ArgumentNullException();
        }

        // Ensure the "uploads" directory exists
        var uploadsDir = _configuration["UploadDirectory"];
        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        string fileExtension = Path.GetExtension(file.FileName);

        // Check file's extension
        if (!fileExtension.Equals(".html"))
        {
            _logger.LogWarning("Provided file has invalid extension: {ext}", fileExtension);
            throw new FormatException();
        }

        var identifier = Guid.NewGuid();
        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
        string uniqueFileName = $"{identifier}{fileExtension}";

        // Combine the uploads folder with the unique filename
        var filePath = Path.Combine(uploadsDir, uniqueFileName);

        // Save the file to the server
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Save file info to database
        var fileDetails = new FileEntity()
        {
            Id = identifier,
            Name = fileName,
            SavedPath = uploadsDir,
            Status = FileStatus.New
        };

        await _dbContext.Files.AddAsync(fileDetails);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<FileDto>(fileDetails);
    }

    public async Task DeleteFileAsync(string id)
    {
        // Find file info at database
        var fileDetail = await _dbContext.Files.FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));

        if (fileDetail == null)
        {
            _logger.LogWarning("No files found at database with the id: {id}", id);
            return;
        }

        // Find all files on the service
        var directoryInfo = new DirectoryInfo(fileDetail.SavedPath!);
        var matchingFiles = directoryInfo.GetFiles($"{fileDetail.Id}.*");

        if (matchingFiles.Length == 0)
        {
            _logger.LogWarning("No files found at host with the name: {id}", id);
            return;
        }

        // Delete every existing file
        foreach (var file in matchingFiles)
        {
            System.IO.File.Delete(file.FullName);
        }

        _dbContext.Files.Remove(fileDetail);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FileDto?> ObtainFileAsync(string id, string extension)
    {
        // Find file info at database
        var fileDetail = await _dbContext.Files.FirstOrDefaultAsync(x => x.Id.ToString().Equals(id));

        if (fileDetail == null)
        {
            _logger.LogWarning("No files found at database with the id: {id}", id);
            return null;
        }

        string filePath = Path.Combine(fileDetail.SavedPath, $"{fileDetail.Id}{extension}");

        if (!File.Exists(filePath))
        {
            _logger.LogWarning("No files found with specified extension: {ext}", extension);
            return null;
        }

        var provider = new FileExtensionContentTypeProvider();

        var result = new FileDto();
        result.Name = $"{fileDetail.Name}{extension}";
        result.Content = await File.ReadAllBytesAsync(filePath);
        if (provider.TryGetContentType(filePath, out string? contentType))
        {
            result.ContentType = contentType;
        }

        return result;
    }
}
