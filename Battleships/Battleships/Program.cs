using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Battleships
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(o =>
                {
                    o.ListenAnyIP(5000);
                })
                .UseStartup<Startup>();
    }
}
