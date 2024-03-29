using DocumentationGenerator.Service.Configurations;
using DocumentationGenerator.Service.Entities;
using DocumentationGenerator.Service.Services.FileManagerService;
using DocumentationGenerator.Service.Services.GeneratorService;
using DocumentationGenerator.Service.Workers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Services
builder.Services.AddTransient<IFileManagerService, CustomFileManagerService>();
builder.Services.AddTransient<IGeneratorService, CustomGeneratorService>();

// Mapper
builder.Services.AddAutoMapper(typeof(FileMapperProfile));

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Workers
builder.Services.AddHostedService<FileConvertWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.MapHub<DataHub>("/files-hub");

app.Run();
