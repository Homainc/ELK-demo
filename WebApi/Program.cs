using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebApi
{
    public class Program
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(HostDefaults.EnvironmentKey)}.json", true,
                    true)
                .Build();
        
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Information("Web host is starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Unable to start web host");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
