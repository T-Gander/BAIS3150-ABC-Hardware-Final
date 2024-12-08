using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class Sales
    {
        static string? _ABCSystemDB;

        public Sales()
        {
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            _ABCSystemDB = DatabaseUsersConfiguration.GetConnectionString("NoSecurity");
        }

        public bool AddSale(Sale sale)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(_ABCSystemDB))
                {
                    SqlCommand AddSaleCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "AddSale",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter SaleDateParameter = new()
                    {
                        ParameterName = "@SaleDate",
                        SqlDbType = SqlDbType.Date,
                        SqlValue = sale.SaleDate
                    };
                    AddSaleCommand.Parameters.Add(SaleDateParameter);

                    SqlParameter SalespersonIDParameter = new()
                    {
                        ParameterName = "@SalespersonID",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = sale.SalespersonID
                    };
                    AddSaleCommand.Parameters.Add(SalespersonIDParameter);

                    SqlParameter CustomerIDParameter = new()
                    {
                        ParameterName = "@CustomerID",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = sale.CustomerID
                    };
                    AddSaleCommand.Parameters.Add(CustomerIDParameter);

                    SqlParameter SubtotalParameter = new()
                    {
                        ParameterName = "@Subtotal",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = sale.Subtotal
                    };
                    AddSaleCommand.Parameters.Add(SubtotalParameter);

                    SqlParameter GSTParameter = new()
                    {
                        ParameterName = "@GST",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = sale.GST
                    };
                    AddSaleCommand.Parameters.Add(GSTParameter);

                    SqlParameter SaleTotalParameter = new()
                    {
                        ParameterName = "@SaleTotal",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = sale.SaleTotal
                    };
                    AddSaleCommand.Parameters.Add(SaleTotalParameter);
                    
                    AddSaleCommand.ExecuteNonQuery();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex.ToString());
            }
            return success;
        }
    }
}
