using DocumentationGenerator.Service.Constants;
using DocumentationGenerator.Service.Dtos;
using DocumentationGenerator.Service.Entities;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;

namespace DocumentationGenerator.Service.Services.GeneratorService;

public class CustomGeneratorService : IGeneratorService
{
    private ILogger<CustomGeneratorService> _logger;
    private readonly DatabaseContext _dbContext;

    public CustomGeneratorService(ILogger<CustomGeneratorService> logger, DatabaseContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task GenerateFileProcessAsync()
    {
        var files = await _dbContext.Files.Where(x => x.Status == FileStatus.New).ToListAsync();

        foreach (var file in files)
        {
            try
            {
                _logger.LogDebug("Start convert process for file {id}", file.Id);

                // Change status of file so that other workers don't use the object
                file.Status = FileStatus.InProgress;
                await _dbContext.SaveChangesAsync();

                await ConvertHtmlToPdfAsync(file);

                file.Status = FileStatus.Converted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while converting file with id: {id}", file.Id);
                file.Status = FileStatus.Error;
            }

            // Update status of file after convert
            _dbContext.Entry(file).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task ConvertHtmlToPdfAsync(FileEntity fileDetail)
    {
        string htmlLocation = Path.Combine(fileDetail.SavedPath, $"{fileDetail.Id}.html");

        if (!File.Exists(htmlLocation))
        {
            _logger.LogWarning("No files found with id: {id} at directory: {dir}", fileDetail.Id, fileDetail.SavedPath);
            return;
        }

        // Convert file
        await new BrowserFetcher().DownloadAsync();

        using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
        using (var page = await browser.NewPageAsync())
        {

            string htmlContent = File.ReadAllText(htmlLocation);
            await page.SetContentAsync(htmlContent);

            string pdfLocation = Path.Combine(fileDetail.SavedPath, $"{fileDetail.Id}.pdf");
            await page.PdfAsync(pdfLocation);
        }

        // Delete original html file
        File.Delete(htmlLocation);
    }
}

