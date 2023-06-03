using Microsoft.AspNetCore.Builder;
using Mpagopay.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.WriteTo.Console()
//.ReadFrom.Configuration(context.Configuration));



var app = builder.ConfigurationService()
                 .ConfigurationPipeline();

//app.UseSerilogRequestLogging();
await app.ResetDatabaseAsync();
app.Run();

public partial class Program { }

