
using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using GrapeCity.ActiveReports.Aspnetcore.Viewer;
using System.Text;
using WebDesigner_Core.Implementation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
builder.Services.AddSingleton<ISharedDataSourceService, SharedDataSourceService>()
            .AddSingleton<IResourcesService, ResourceService>()
            .AddReporting()
            .AddDesigner()
            .AddSingleton<IDataSetsService>(new CustomDataSetTemplates());
WebApplication app = builder.Build();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
}
// Configure middleware for ActiveReports API and handlers.
IResourcesService resourcesService = app.Services.GetRequiredService<IResourcesService>();
ISharedDataSourceService sharedDataSourceService = app.Services.GetRequiredService<ISharedDataSourceService>();
app.UseDesigner(config =>
{
    config.UseCustomStore(resourcesService);
    config.UseSharedDataSources(sharedDataSourceService);
    config.UseDataSetTemplates(app.Services.GetRequiredService<IDataSetsService>());
});
app.UseReporting(config =>
{
    config.UseCustomStore(resourcesService.GetReport);
});
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();

public partial class Program
{
    public static readonly DirectoryInfo ResourcesRootDirectory = new(Path.Combine(Directory.GetCurrentDirectory(), "resources" + Path.DirectorySeparatorChar));
}