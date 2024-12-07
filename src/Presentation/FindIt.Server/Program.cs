using FindIt.Server;
using FindIt.Server.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationDependencies();

var app = builder.Build();

app.UseSwaggerMiddleware();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
