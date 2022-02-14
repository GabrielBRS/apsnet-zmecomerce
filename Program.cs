using ecommerce_aspnetmvc_entityframework.Libraries.Bug;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ecommerce_aspnetmvc_entityframework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory(); // call it here
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
