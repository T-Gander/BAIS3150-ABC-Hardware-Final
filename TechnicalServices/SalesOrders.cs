﻿namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class SalesOrders
    {
        static string? _ABCSystemDB;

        public SalesOrders()
        {
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            _ABCSystemDB = DatabaseUsersConfiguration.GetConnectionString("NoSecurity");
        }
    }
}