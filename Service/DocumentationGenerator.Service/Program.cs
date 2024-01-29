using DocumentationGenerator.Service.Configurations;
using DocumentationGenerator.Service.Entities;
using DocumentationGenerator.Service.Services.FileManagerService;
using DocumentationGenerator.Service.Services.GeneratorService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Services
builder.Services.AddTransient<IFileManagerService, CustomFileManagerService>();
builder.Services.AddSingleton<IGeneratorService, CustomerGeneratorService>();

// Mapper
builder.Services.AddAutoMapper(typeof(FileMapperProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
