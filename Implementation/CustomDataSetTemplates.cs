using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;
using WebDesigner_Core.DataSets;

namespace WebDesigner_Core.Implementation
{
    public class CustomDataSetTemplates : IDataSetsService
    {
        public DataSetTemplateInfo[] GetDataSetsList()
        {
            DataSetTemplateInfo[] dataSetsList = CustomDataSetTemplatesStore.Items
                .Select(i => new DataSetTemplateInfo
                {
                    Id = i.Key,
                    Name = i.Key
                })
                .ToArray();

            return dataSetsList;
        }

        public DataSetTemplate GetDataSet(string id)
        {
            _ = CustomDataSetTemplatesStore.Items.TryGetValue(id, out DataSetTemplate dataSet);
            return dataSet;
        }
    }
}
