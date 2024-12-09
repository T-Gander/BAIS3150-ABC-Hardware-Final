using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class Sales : SqlConnectionObject
    {
        public int AddSale(Sale sale)
        {
            int saleNumber = 0;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    Connection.Open();

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

                    SqlParameter SaleNumberParameter = new()
                    {
                        ParameterName = "@SaleNumber",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    AddSaleCommand.Parameters.Add(SaleNumberParameter);
                    
                    AddSaleCommand.ExecuteNonQuery();

                    saleNumber = (int)SaleNumberParameter.Value;
                }
            }
            catch (Exception ex)
            {
                saleNumber = 0;
                Console.WriteLine(ex.ToString());
            }
            return saleNumber;
        }
    }
}
