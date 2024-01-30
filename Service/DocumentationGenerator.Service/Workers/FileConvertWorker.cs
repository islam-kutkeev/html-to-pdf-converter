using DocumentationGenerator.Service.Services.GeneratorService;

namespace DocumentationGenerator.Service.Workers;

public class FileConvertWorker : BackgroundService
{
    private readonly ILogger<FileConvertWorker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScope;

    public FileConvertWorker(ILogger<FileConvertWorker> logger, IConfiguration configuration, IServiceScopeFactory serviceScope)
    {
        _logger = logger;
        _configuration = configuration;
        _serviceScope = serviceScope;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var generatorService = scope.ServiceProvider.GetService<IGeneratorService>();
                    await generatorService.GenerateFileProcessAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(int.Parse(_configuration["WorkerPollingRateInSec"])), stoppingToken);
            }
        }
        catch (Exception) when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogWarning("Execution of file convert worker ended. Cancellation war requested.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Execution of file convert worker ended with unhandled exception");
        }
    }
}
