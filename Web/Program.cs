using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Web.Data;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:80",
                                       "https://localhost:556",
                                       "https://localhost:5001",
                                       "http://192.168.88.27:80",
                                       "https://192.168.88.27:556"
                                       );
                    webBuilder.UseStartup<Startup>();
                });
    }
}
