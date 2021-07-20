using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AnalyticsEventAPIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Rook.RookOptions options = new Rook.RookOptions() 
            {
                token = "3a004a5e9fdb62fcb9a2d0a3b4308810e169f050c42a8903ebdcc04d61077c5d",
                labels = new Dictionary<string, string> { { "env", "NetCore-AnalyticsAPIs" } }
            };
            Rook.API.Start(options);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
 
                  config.AddJsonFile(
                  "appsettings.json", optional: false, reloadOnChange: false);
 
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
