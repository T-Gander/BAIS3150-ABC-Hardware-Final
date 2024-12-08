using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class SalesOrders : SqlConnectionObject
    {
        public bool AddSaleOrder(SaleOrder saleOrder)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand AddSaleOrderCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "AddSaleOrder",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter SaleNumberParameter = new()
                    {
                        ParameterName = "@SaleNumber",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = saleOrder.SaleNumber
                    };
                    AddSaleOrderCommand.Parameters.Add(SaleNumberParameter);

                    SqlParameter ItemNumberParameter = new()
                    {
                        ParameterName = "@ItemNumber",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = saleOrder.ItemNumber
                    };
                    AddSaleOrderCommand.Parameters.Add(ItemNumberParameter);

                    SqlParameter QuantityParameter = new()
                    {
                        ParameterName = "@Quantity",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = saleOrder.Quantity
                    };
                    AddSaleOrderCommand.Parameters.Add(QuantityParameter);

                    SqlParameter ItemTotalParameter = new()
                    {
                        ParameterName = "@ItemTotal",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = saleOrder.ItemTotal
                    };
                    AddSaleOrderCommand.Parameters.Add(ItemTotalParameter);

                    AddSaleOrderCommand.ExecuteNonQuery();
                    
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
