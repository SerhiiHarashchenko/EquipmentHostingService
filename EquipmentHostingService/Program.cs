using EquipmentHostingService.BackgroundProcessing;
using EquipmentHostingService.Data;
using EquipmentHostingService.Data.Repositories;
using EquipmentHostingService.Mapping;
using EquipmentHostingService.Middleware;
using EquipmentHostingService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
builder.Services.AddDbContext<EquipmentHostingServiceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<IMessageQueue, InMemoryMessageQueue>();
builder.Services.AddHostedService<BackgroundProcessor>();

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IContractService, ContractsService>();
builder.Services.AddSingleton<BackgroundLoggingService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
