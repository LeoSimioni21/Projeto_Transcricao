using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto_Transcricao_C_.data;
using Projeto_Transcricao_C_.entities;
using Projeto_Transcricao_C_.repositories;
using Projeto_Transcricao_C_.services;
using Projeto_Transcricao_C_.settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Projeto Transcrição API",
        Version = "v1"
    });

    
    c.OperationFilter<FileUploadOperationFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<OpenAiSettings>(
    builder.Configuration.GetSection("OpenAi")
    );

builder.Services.AddScoped<ITranscriptionService, TranscriptionService>();
builder.Services.AddScoped<ITranscriptionRepository, TranscriptionRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
