using Microsoft.EntityFrameworkCore;
using NotificationSubscriber;

var builder = WebApplication.CreateBuilder(args);

// Reads the 'FhirEngine' configuration section to add services
builder.AddFhirEngineServer();

// Add DbContext with connection string
builder.Services.AddDbContext<TestResultDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestResultDb")));

var app = builder.Build();

app.UseHsts()
    .UseStaticFiles() // for serving rapidoc index.html
    .UseRouting()
    .UseFhirEngineMiddlewares()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapFhirEngineHealthChecks("/health");
        endpoints.MapFhirEngine();
    });

await app.RunAsync();
