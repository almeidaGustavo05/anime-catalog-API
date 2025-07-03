using AnimeCatalog.API.Middleware;
using AnimeCatalog.Application;
using AnimeCatalog.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/anime-catalog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação falhou ao iniciar");
}
finally
{
    Log.CloseAndFlush();
}
