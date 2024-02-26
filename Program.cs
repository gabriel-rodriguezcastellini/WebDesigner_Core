
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
DirectoryInfo ResourcesRootDirectory = new(Path.Combine(Directory.GetCurrentDirectory(), "resources" + Path.DirectorySeparatorChar));
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDesigner();
WebApplication app = builder.Build();
app.UseHttpsRedirection();
if (!app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
}
// Configure middleware for ActiveReports API and handlers.
app.UseDesigner(config => config.UseFileStore(ResourcesRootDirectory, false));
app.UseReporting(config => config.UseFileStore(ResourcesRootDirectory));
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();

