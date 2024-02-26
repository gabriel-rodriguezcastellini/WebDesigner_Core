using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;
using GrapeCity.ActiveReports.PageReportModel;
using System.ComponentModel;

namespace WebDesigner_Core.Implementation
{
    public class ResourceService : IResourcesService
    {
        private readonly Dictionary<string, Report> _tempStorage = [];
        private readonly ResourceLocator _resourceLocator;

        public ResourceService()
        {
            _resourceLocator = new DefaultResourceLocator(new Uri(Program.ResourcesRootDirectory.FullName));
        }

        public Report GetReport(string id)
        {
            if (_tempStorage.TryGetValue(id, out Report? tempReport))
            {
                tempReport.Site = new ReportSite(_resourceLocator);
                return tempReport;
            }

            string reportFullPath = Path.Combine(Program.ResourcesRootDirectory.FullName, id);
            byte[] reportXml = File.ReadAllBytes(reportFullPath);
            Report report = ReportConverter.FromXML(reportXml);
            report.Site = new ReportSite(_resourceLocator);

            return report;
        }

        public IReportInfo[] GetReportsList()
        {
            return Program.ResourcesRootDirectory
                .EnumerateFiles("*.rdlx")
                .Select(x =>
                {
                    byte[] reportXml = File.ReadAllBytes(x.FullName);
                    Report report = ReportConverter.FromXML(reportXml);
                    return new ReportInfo
                    {
                        Id = x.Name,
                        Name = x.Name,
                        Type = report.IsFixedPageReport ? "FPL" : "CPL"
                    };
                }).Cast<IReportInfo>().ToArray();
        }

        public string SaveReport(string name, Report report, bool isTemporary = false)
        {
            if (isTemporary)
            {
                string tempName = Guid.NewGuid() + ".rdlx";
                _tempStorage.Add(tempName, report);
                return tempName;
            }

            string reportFullPath = Path.Combine(Program.ResourcesRootDirectory.FullName, name);
            report.Name = name;
            byte[] reportXml = ReportConverter.ToXml(report);
            File.WriteAllBytes(reportFullPath, reportXml);
            return name;
        }

        public string UpdateReport(string id, Report report)
        {
            return SaveReport(id, report, false);
        }

        public void DeleteReport(string id)
        {
            if (_tempStorage.ContainsKey(id))
            {
                _ = _tempStorage.Remove(id);
                return;
            }

            string reportFullPath = Path.Combine(Program.ResourcesRootDirectory.FullName, id);

            if (File.Exists(reportFullPath))
            {
                File.Delete(reportFullPath);
            }
        }

        public Uri GetBaseUri()
        {
            return _resourceLocator.BaseUri;
        }

        public Theme GetTheme(string id)
        {
            throw new NotImplementedException();
        }

        public IThemeInfo[] GetThemesList()
        {
            return Array.Empty<IThemeInfo>();
        }

        public byte[] GetImage(string id, out string mimeType)
        {
            throw new NotImplementedException();
        }

        public IImageInfo[] GetImagesList()
        {
            return Array.Empty<IImageInfo>();
        }
    }

    public class ReportInfo : IReportInfo
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Type { get; set; }
    }

    internal class ReportSite : ISite
    {
        private readonly ResourceLocator _resourceLocator;

        public ReportSite(ResourceLocator resourceLocator)
        {
            _resourceLocator = resourceLocator;
        }

        public object? GetService(Type serviceType)
        {
            return serviceType == typeof(ResourceLocator) ? _resourceLocator : null;
        }

        public IComponent? Component => null;

        public IContainer? Container => null;

        public bool DesignMode => false;

        public string Name { get; set; }
    }
}
