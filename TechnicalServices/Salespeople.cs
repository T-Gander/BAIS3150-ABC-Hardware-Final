using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class Salespeople : SqlConnectionObject
    {
        public Salesperson GetSalesperson(int SalespersonID)
        {
            Salesperson salesperson = new();

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    Connection.Open();

                    SqlCommand GetSalespersonCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "GetSalesperson",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter SalespersonIDParameter = new()
                    {
                        ParameterName = "@SalespersonID",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = SalespersonID
                    };
                    GetSalespersonCommand.Parameters.Add(SalespersonIDParameter);

                    SqlDataReader GetSalespersonReader = GetSalespersonCommand.ExecuteReader();

                    while (GetSalespersonReader.Read())
                    {
                        salesperson = new()
                        {
                            SalespersonID = (int)GetSalespersonReader["SalespersonID"],
                            FirstName = (string)GetSalespersonReader["FirstName"],
                            LastName = (string)GetSalespersonReader["LastName"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return salesperson;
        }

        public List<Salesperson> GetSalespeople()
        {
            List<Salesperson> salespeople = new();

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    Connection.Open();

                    SqlCommand GetSalespeopleCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "GetSalespeople",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader GetSalespeopleReader = GetSalespeopleCommand.ExecuteReader();

                    while (GetSalespeopleReader.Read())
                    {
                        Salesperson salesperson = new()
                        {
                            SalespersonID = (int)GetSalespeopleReader["SalespersonID"],
                            FirstName = (string)GetSalespeopleReader["FirstName"],
                            LastName = (string)GetSalespeopleReader["LastName"]
                        };

                        salespeople.Add(salesperson);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                salespeople = new();
            }
            return salespeople;
        }
    }
}
