using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace AMVACChemical
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             //.UseKestrel(options =>
             //{
             //    options.Listen(IPAddress.Loopback, 5000); //HTTP port
             //})
             .UseUrls(urls: "https://localhost:5001")
            .UseStartup<Startup>();
    }
}
