using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class SaleItems : SqlConnectionObject
    {
        public bool AddSaleItem(SaleItem saleOrder)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    Connection.Open();

                    SqlCommand AddSaleItemCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "AddSaleItem",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter SaleNumberParameter = new()
                    {
                        ParameterName = "@SaleNumber",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = saleOrder.SaleNumber
                    };
                    AddSaleItemCommand.Parameters.Add(SaleNumberParameter);

                    SqlParameter ItemNumberParameter = new()
                    {
                        ParameterName = "@ItemNumber",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = saleOrder.ItemNumber
                    };
                    AddSaleItemCommand.Parameters.Add(ItemNumberParameter);

                    SqlParameter QuantityParameter = new()
                    {
                        ParameterName = "@Quantity",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = saleOrder.Quantity
                    };
                    AddSaleItemCommand.Parameters.Add(QuantityParameter);

                    SqlParameter ItemTotalParameter = new()
                    {
                        ParameterName = "@ItemTotal",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = saleOrder.ItemTotal
                    };
                    AddSaleItemCommand.Parameters.Add(ItemTotalParameter);

                    AddSaleItemCommand.ExecuteNonQuery();
                    
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
