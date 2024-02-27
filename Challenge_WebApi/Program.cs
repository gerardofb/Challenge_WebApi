using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Queries.Implementation;
using Queries.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Infrastructure.ElasticViewModels;
using Repository.UnitOfWork;
using Nest;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.File("logs/challengewebapi_log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
// Add services to the container.
QueryElasticPermissions queryElasticPermissions = new QueryElasticPermissions(configuration);
UnitOfWorkElasticPermissions unitOfWorkElastic = new UnitOfWorkElasticPermissions(configuration);
//ElasticRepositoryPermissions<ViewModelElasticPermissionsUser> _elasticSearchRepo = new ElasticRepositoryPermissions<ViewModelElasticPermissionsUser>(configuration); 
builder.Services.AddSingleton<UnitOfWorkElasticPermissions>(unitOfWorkElastic);
builder.Services.AddDbContext<ChallengeContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IQueryPermissions, QueryPermissions>();
//builder.Services.AddControllers();
builder.Host.UseSerilog();

builder.Services.AddSingleton<QueryElasticPermissions>(queryElasticPermissions);
builder.Services.AddMvc();
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
