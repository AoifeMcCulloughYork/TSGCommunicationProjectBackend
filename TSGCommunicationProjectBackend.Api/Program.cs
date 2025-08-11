using Microsoft.EntityFrameworkCore;
using TSGCommunicationProjectBackend.Data.Repositories.Interfaces;
using TSGCommunicationProjectBackend.Data.Repositories;
using TSGCommunicationProjectBackend.Common.Interfaces;
using TSGCommunicationProjectBackend.Service;
using TSGCommunicationProjectBackend.Data.Contexts;
using static TSGCommunicationProjectBackend.Data.DbInit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
    builder =>
    {
        builder.WithOrigins("https://localhost:7268", "http://localhost:7268", "https:/localhost:7018", "http://localhost:7018")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommunicationRepository, CommunicationRepository>();
builder.Services.AddScoped<ICommunicationTypeRepository, CommunicationTypeRepository>();
builder.Services.AddScoped<ICommunicationTypeStatusRepository, CommunicationTypeStatusRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services.AddScoped<ICommunicationTypesService, CommunicationTypeService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await InitializeAsync(scope.ServiceProvider);
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowBlazorApp");
app.MapControllers();

app.Run();

