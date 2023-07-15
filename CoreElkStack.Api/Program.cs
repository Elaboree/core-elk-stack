using Serilog;
using Serilog.Formatting.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();

ConfigureLogging();
builder.Host.UseSerilog();

//Log.ForContext<StartupBase>()
//               .Information("<{EventID:l}> Configure Started on {Env} {Application} with {@configuration}",
//                   "Startup", builder.Environment.EnvironmentName, builder.Environment.ApplicationName, builder.Configuration);

var app = builder.Build();

Log.Information("Web api starting...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureLogging()
{
    Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "CoreElkStack.Api")
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Elasticsearch(
            new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(
                new Uri("http://localhost:9200/"))
            {
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                AutoRegisterTemplate = true,
                TemplateName = "serilog-events-template",
                IndexFormat = "core-elastic-stack-log-{0:yyyy.MM.dd}",
                ModifyConnectionSettings = cred => cred.BasicAuthentication("elastic", "changeme")
            }
)
    .MinimumLevel.Verbose()
    .CreateLogger();
}
