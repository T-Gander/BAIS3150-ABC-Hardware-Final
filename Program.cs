namespace BAIS3150_ABC_Hardware_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            builder.WebHost.UseStaticWebAssets();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error"); //for production
            }

            app.MapRazorPages();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseSession();

            app.Run();
        }
    }
}
