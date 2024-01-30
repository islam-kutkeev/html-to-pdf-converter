using DocumentationGenerator.Service.Dtos;

namespace DocumentationGenerator.Service.Services.GeneratorService;

public interface IGeneratorService
{
    /// <summary>
    /// Starts process of converting files from html to pdf
    /// </summary>
    public Task GenerateFileProcessAsync();
}
