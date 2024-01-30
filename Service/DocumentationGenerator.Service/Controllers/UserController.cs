using DocumentationGenerator.Service.Dtos;
using DocumentationGenerator.Service.Services.FileManagerService;
using Microsoft.AspNetCore.Mvc;

namespace DocumentationGenerator.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IFileManagerService _fileManager;


    public UserController(ILogger<UserController> logger, IFileManagerService fileManager)
    {
        _logger = logger;
        _fileManager = fileManager;
    }

    [HttpGet("/get-all")]
    public async Task<ResponseDto<List<FileDto>>> GetAllFiles()
    {
        try
        {
            return new ResponseDto<List<FileDto>>(await _fileManager.GetAllFilesAsync());
        }
        catch (Exception ex)
        {
            string message = "Error occurred while trying to obtain files list";
            _logger.LogError(ex, message);

            return new ResponseDto<List<FileDto>>(5, message, null);
        }
    }

    [HttpPost("/save")]
    public async Task<ResponseDto<FileDto>> SaveNewFile(IFormFile file)
    {
        var result = new ResponseDto<FileDto>();
        try
        {
            result.Data = await _fileManager.SaveFileAsync(file);
        }
        catch (FormatException ex)
        {
            result.Code = 1;
            result.Message = "Provided file format was invalid";
            _logger.LogError(ex, result.Message);
        }
        catch (Exception ex)
        {
            result.Code = 5;
            result.Message = "Error occurred while trying to save file at server";
            _logger.LogError(ex, result.Message);
        }

        return result;
    }

    [HttpDelete("/delete/{id}")]
    public async Task<ResponseDto> DeleteExistingFile(string id)
    {
        try
        {
            await _fileManager.DeleteFileAsync(id);
            return new ResponseDto();
        }
        catch (Exception ex)
        {
            string message = String.Format("Error occurred while trying to delete file at server with id: {id}", id);
            _logger.LogError(ex, message);
            return new ResponseDto<FileDto>(5, message, null);
        }
    }

    [HttpGet("/download/{id}")]
    public async Task<IActionResult> DownloadExistingFile(string id)
    {
        try
        {
            var fileInfo = await _fileManager.ObtainFileAsync(id, ".html");
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            return File(fileInfo.Content, fileInfo.ContentType, fileInfo.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while trying to download file with id: {id}", id);
        }

        return BadRequest();
    }
}
