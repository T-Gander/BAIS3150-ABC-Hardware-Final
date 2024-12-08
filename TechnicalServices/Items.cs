using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class Items : SqlConnectionObject
    {
        public bool AddItem(Item item)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand AddItemCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "AddItem",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter ItemNumberParameter = new()
                    {
                        ParameterName = "@ItemNumber",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = item.ItemNumber
                    };
                    AddItemCommand.Parameters.Add(ItemNumberParameter);

                    SqlParameter DescriptionParameter = new()
                    {
                        ParameterName = "@Description",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = item.Description
                    };
                    AddItemCommand.Parameters.Add(DescriptionParameter);

                    SqlParameter UnitPriceParameter = new()
                    {
                        ParameterName = "@UnitPrice",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = item.UnitPrice
                    };
                    AddItemCommand.Parameters.Add(UnitPriceParameter);

                    SqlParameter QuantityOnHandParameter = new()
                    {
                        ParameterName = "@QuantityOnHand",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = item.QuantityOnHand
                    };
                    AddItemCommand.Parameters.Add(QuantityOnHandParameter);

                    AddItemCommand.ExecuteNonQuery();

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

        public bool DeleteItem(int ItemNumber)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand DeleteItemCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "DeleteItem",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter ItemNumberParameter = new()
                    {
                        ParameterName = "@ItemNumber",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = ItemNumber
                    };
                    DeleteItemCommand.Parameters.Add(ItemNumberParameter);

                    DeleteItemCommand.ExecuteNonQuery();

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

        public bool UpdateItem(Item item)
        {
            bool success = false;

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand UpdateItemCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "UpdateItem",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter ItemNumberParameter = new()
                    {
                        ParameterName = "@ItemNumber",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = item.ItemNumber
                    };
                    UpdateItemCommand.Parameters.Add(ItemNumberParameter);

                    SqlParameter DescriptionParameter = new()
                    {
                        ParameterName = "@Description",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = item.Description
                    };
                    UpdateItemCommand.Parameters.Add(DescriptionParameter);

                    SqlParameter UnitPriceParameter = new()
                    {
                        ParameterName = "@UnitPrice",
                        SqlDbType = SqlDbType.Money,
                        SqlValue = item.UnitPrice
                    };
                    UpdateItemCommand.Parameters.Add(UnitPriceParameter);

                    SqlParameter QuantityOnHandParameter = new()
                    {
                        ParameterName = "@QuantityOnHand",
                        SqlDbType = SqlDbType.Int,
                        SqlValue = item.QuantityOnHand
                    };
                    UpdateItemCommand.Parameters.Add(QuantityOnHandParameter);

                    UpdateItemCommand.ExecuteNonQuery();

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

        public Item GetItem(int ItemNumber)
        {
            Item item = new();

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand GetItemCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "GetItem",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter ItemNumberParameter = new()
                    {
                        ParameterName = "@ItemNumber",
                        SqlDbType = SqlDbType.VarChar,
                        SqlValue = item.ItemNumber
                    };
                    GetItemCommand.Parameters.Add(ItemNumberParameter);

                    SqlDataReader GetItemReader = GetItemCommand.ExecuteReader();

                    while (GetItemReader.Read()) 
                    {
                        item = new()
                        {
                            ItemNumber = (string)GetItemReader["ItemNumber"],
                            Description = (string)GetItemReader["Description"],
                            UnitPrice = (decimal)GetItemReader["UnitPrice"],
                            Deleted = (bool)GetItemReader["Deleted"],
                            QuantityOnHand = (int)GetItemReader["QuantityOnHand"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                item = new();
                Console.WriteLine(ex.ToString());
            }
            return item;
        }

        public List<Item> GetItems()
        {
            List<Item> items = new();

            try
            {
                using (SqlConnection Connection = new(ABCSystemDB))
                {
                    SqlCommand GetItemsCommand = new()
                    {
                        Connection = Connection,
                        CommandText = "GetItems",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader GetItemReader = GetItemsCommand.ExecuteReader();

                    while (GetItemReader.Read())
                    {
                        Item item = new()
                        {
                            ItemNumber = (string)GetItemReader["ItemNumber"],
                            Description = (string)GetItemReader["Description"],
                            UnitPrice = (decimal)GetItemReader["UnitPrice"],
                            Deleted = (bool)GetItemReader["Deleted"],
                            QuantityOnHand = (int)GetItemReader["QuantityOnHand"]
                        };

                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                items = new();
                Console.WriteLine(ex.ToString());
            }
            return items;
        }
    }
}
