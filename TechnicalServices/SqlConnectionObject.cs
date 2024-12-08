namespace BAIS3150_ABC_Hardware_Final.TechnicalServices
{
    public class SqlConnectionObject
    {
        public static string? ABCSystemDB;

        public SqlConnectionObject()
        {
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            ABCSystemDB = DatabaseUsersConfiguration.GetConnectionString("NoSecurity");
        }
    }
}
