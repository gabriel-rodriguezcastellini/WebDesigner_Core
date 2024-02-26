using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.ActiveReports.Rendering.Tools;

namespace WebDesigner_Core.Implementation
{
    public class SharedDataSourceService : ISharedDataSourceService
    {
        public SharedDataSourceInfo[] GetSharedDataSourceList()
        {
            return Program.ResourcesRootDirectory
                .EnumerateFiles("*.rdsx")
                .Select(x =>
                {
                    DataSource dataSource = GetDataSource(x.Name);
                    return new SharedDataSourceInfo()
                    {
                        Name = x.Name,
                        Type = dataSource.ConnectionProperties.DataProvider
                    };
                }).ToArray();
        }

        public DataSource GetDataSource(string name)
        {
            return DataSourceTools.LoadSharedDataSource(Path.Combine(Program.ResourcesRootDirectory.FullName, name));
        }
    }
}
