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
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration;
if (!builder.Environment.IsDevelopment())
{
    configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
}
else
{
    configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings_dev.json").Build();
}
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.File("logs/challengewebapi_log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
// Add services to the container.
QueryElasticPermissions queryElasticPermissions = new QueryElasticPermissions(configuration);
UnitOfWorkElasticPermissions unitOfWorkElastic = new UnitOfWorkElasticPermissions(configuration);
builder.Services.AddSingleton<UnitOfWorkElasticPermissions>(unitOfWorkElastic);
builder.Services.AddDbContext<ChallengeContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IQueryPermissions, QueryPermissions>();
builder.Host.UseSerilog();


builder.Services.AddSingleton<QueryElasticPermissions>(queryElasticPermissions);
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CONFIGURE SSL CONNECTION
//if (!builder.Environment.IsDevelopment())
//{
//    builder.WebHost.ConfigureKestrel(kestrel => kestrel.ConfigureHttpsDefaults(opt =>
// opt.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13));
//    builder.Services.AddHttpsRedirection(options =>
//    {
//        options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
//        options.HttpsPort = 443;
//    });
//}
var app = builder.Build();
// CONFIGURE SSL CONNECTION
//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChallengeContext>();
    //db.Database.EnsureDeleted();
    //db.Database.EnsureCreated();
    db.Database.Migrate();
}
app.Run();
public partial class Program { }