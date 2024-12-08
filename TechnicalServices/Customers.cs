using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class Customers : SqlConnectionObject
    {
        public bool AddCustomer(Customer customer)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand AddCustomerCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "AddCustomer",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter FirstNameParameter = new()
                    {
                        ParameterName = "@FirstName",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.FirstName
                    };
                    AddCustomerCommand.Parameters.Add(FirstNameParameter);

                    SqlParameter LastNameParameter = new()
                    {
                        ParameterName = "@LastName",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.LastName
                    };
                    AddCustomerCommand.Parameters.Add(LastNameParameter);

                    SqlParameter AddressParameter = new()
                    {
                        ParameterName = "@Address",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.Address
                    };
                    AddCustomerCommand.Parameters.Add(AddressParameter);

                    SqlParameter CityParameter = new()
                    {
                        ParameterName = "@City",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.City
                    };
                    AddCustomerCommand.Parameters.Add(CityParameter);

                    SqlParameter ProvinceParameter = new()
                    {
                        ParameterName = "@Province",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.Province
                    };
                    AddCustomerCommand.Parameters.Add(ProvinceParameter);

                    SqlParameter PostalCodeParameter = new()
                    {
                        ParameterName = "@PostalCode",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.PostalCode
                    };
                    AddCustomerCommand.Parameters.Add(PostalCodeParameter);

                    AddCustomerCommand.ExecuteNonQuery();

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

        public bool DeleteCustomer(int CustomerID)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand DeleteCustomerCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "DeleteCustomer",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter CustomerIDParameter = new()
                    {
                        ParameterName = "@CustomerID",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = CustomerID
                    };
                    DeleteCustomerCommand.Parameters.Add(CustomerIDParameter);

                    DeleteCustomerCommand.ExecuteNonQuery();

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

        public Customer GetCustomer(int CustomerID)
        {
            Customer customer = new();

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand GetCustomerCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "GetCustomer",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter CustomerIDParameter = new()
                    {
                        ParameterName = "@CustomerID",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = CustomerID
                    };
                    GetCustomerCommand.Parameters.Add(CustomerIDParameter);

                    SqlDataReader GetCustomerReader = GetCustomerCommand.ExecuteReader();

                    while (GetCustomerReader.Read()) 
                    {
                        customer = new()
                        {
                            CustomerID = (int)GetCustomerReader["CustomerID"],
                            FirstName = (string)GetCustomerReader["FirstName"],
                            LastName = (string)GetCustomerReader["LastName"],
                            Address = (string)GetCustomerReader["Address"],
                            City = (string)GetCustomerReader["City"],
                            Province = (string)GetCustomerReader["Province"],
                            PostalCode = (string)GetCustomerReader["PostalCode"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return customer;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new();

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand GetCustomersCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "GetCustomers",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader GetCustomersReader = GetCustomersCommand.ExecuteReader();

                    while (GetCustomersReader.Read())
                    {
                        Customer customer = new()
                        {
                            CustomerID = (int)GetCustomersReader["CustomerID"],
                            FirstName = (string)GetCustomersReader["FirstName"],
                            LastName = (string)GetCustomersReader["LastName"],
                            Address = (string)GetCustomersReader["Address"],
                            City = (string)GetCustomersReader["City"],
                            Province = (string)GetCustomersReader["Province"],
                            PostalCode = (string)GetCustomersReader["PostalCode"]
                        };
                        customers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                customers = new();
            }
            return customers;
        }

        public bool UpdateCustomer(Customer customer)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand UpdateCustomerCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "UpdateCustomer",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter CustomerIDParameter = new()
                    {
                        ParameterName = "@CustomerID",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = customer.CustomerID
                    };
                    UpdateCustomerCommand.Parameters.Add(CustomerIDParameter);

                    SqlParameter FirstNameParameter = new()
                    {
                        ParameterName = "@FirstName",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.FirstName
                    };
                    UpdateCustomerCommand.Parameters.Add(FirstNameParameter);

                    SqlParameter LastNameParameter = new()
                    {
                        ParameterName = "@LastName",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.LastName
                    };
                    UpdateCustomerCommand.Parameters.Add(LastNameParameter);

                    SqlParameter AddressParameter = new()
                    {
                        ParameterName = "@Address",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.Address
                    };
                    UpdateCustomerCommand.Parameters.Add(AddressParameter);

                    SqlParameter CityParameter = new()
                    {
                        ParameterName = "@City",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.City
                    };
                    UpdateCustomerCommand.Parameters.Add(CityParameter);

                    SqlParameter ProvinceParameter = new()
                    {
                        ParameterName = "@Province",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.Province
                    };
                    UpdateCustomerCommand.Parameters.Add(ProvinceParameter);

                    SqlParameter PostalCodeParameter = new()
                    {
                        ParameterName = "@PostalCode",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = customer.PostalCode
                    };
                    UpdateCustomerCommand.Parameters.Add(PostalCodeParameter);

                    UpdateCustomerCommand.ExecuteNonQuery();

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
