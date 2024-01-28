using Microsoft.AspNetCore.Mvc;

namespace DocumentationGenerator.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("/get-all")]
    public async Task GetAllFiles(){
        throw new NotImplementedException();
    }

    [HttpPost("/save")]
    public async Task SaveNewFile(){
        throw new NotImplementedException();
    }

    [HttpDelete("/delete/{id}")]
    public async Task DeleteExistingFile(){
        throw new NotImplementedException();
    }

    [HttpGet("/download/{id}")]
    public async Task DownloadExistingFile(){
        throw new NotImplementedException();
    }
}
