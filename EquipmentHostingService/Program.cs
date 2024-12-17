using EquipmentHostingService.Data;
using EquipmentHostingService.Data.Repositories;
using EquipmentHostingService.Mapping;
using EquipmentHostingService.Middleware;
using EquipmentHostingService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EquipmentHostingServiceDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IContractService, ContractsService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
