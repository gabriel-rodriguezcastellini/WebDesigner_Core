using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.PageReportModel;

namespace WebDesigner_Core.DataSets
{
    public static class CustomDataSetTemplatesStore
    {
        public static IDictionary<string, DataSetTemplate> Items { get; }

        static CustomDataSetTemplatesStore()
        {
            Items = new Dictionary<string, DataSetTemplate>()
            {
                {
                    "Sales",
                    new DataSetTemplate
                    {
                        DataSource = new DataSource
                        {
                            Name = "AdventureWorks2022",
                            ConnectionProperties =
                            {
                                DataProvider = "SQL",
                                ConnectString = "Data Source=LAPTOP-75PLAUOF;Initial Catalog=AdventureWorks2022;Integrated Security=True;TrustServerCertificate=True"
                            }
                        },
                        DataSet = new DataSet
                        {
                            Name = "Sales",
                            Query =
                            {
                                CommandText = "select soh.OrderDate as Date, soh.SalesOrderNumber as [Order], pps.Name as Subcat, pp.Name as Product, SUM(sd.OrderQty) as Qty, SUM(sd.LineTotal) as LineTotal\nfrom Sales.SalesPerson sp inner join Sales.SalesOrderHeader soh on sp.BusinessEntityID = soh.SalesPersonID inner join Sales.SalesOrderDetail sd on sd.SalesOrderID = soh.SalesOrderID inner join Production.Product pp on sd.ProductID = pp.ProductID inner join Production.ProductSubcategory pps on pp.ProductSubcategoryID = pps.ProductSubcategoryID inner join Production.ProductCategory ppc on ppc.ProductCategoryID = pps.ProductCategoryID\ngroup by ppc.Name, soh.OrderDate, soh.SalesOrderNumber, pps.Name, pp.Name, soh.SalesPersonID\nhaving ppc.Name = 'Clothing'",
                                DataSourceName = "AdventureWorks2022"
                            },
                            Fields =
                            {
                                new Field
                                {
                                    Name = "Date",
                                    DataField = "Date"
                                },
                                new Field
                                {
                                    Name = "Order",
                                    DataField = "Order"
                                },
                                new Field
                                {
                                    Name = "Subcat",
                                    DataField = "Subcat"
                                },
                                new Field
                                {
                                    Name = "Product",
                                    DataField = "Product"
                                },
                                new Field
                                {
                                    Name = "Qty",
                                    DataField = "Qty"
                                },
                                new Field
                                {
                                    Name = "LineTotal",
                                    DataField = "LineTotal"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
