using Microservices.IDP.Extensions;
using Microservices.IDP.Persistence;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);

try
{
  

    builder.Host.AddAppConfigurations();

    builder.Host.ConfigureSerilog();

    builder.Services.ConfigureCookiePolicy();


    var app = builder
        .ConfigureServices()
        .ConfigurePipeline()
        ;

    app.MigrateDatabase();

   // if(app.Environment.IsDevelopment())
   //  {
        SeedUserData.EnsureSeedData(builder.Configuration.GetConnectionString("IdentitySqlConnection"));
   // }


    app.Run();
}
catch (Exception ex)
{

    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shut down  complete");
    Log.CloseAndFlush();
}
