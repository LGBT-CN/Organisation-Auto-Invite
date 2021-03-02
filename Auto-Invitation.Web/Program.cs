using System;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Auto_Invitation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Shared.Config = JsonSerializer.Deserialize<Models.ConfigModel>(File.ReadAllText("config.json"),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            if (Shared.Config == null)
                throw new ArgumentNullException("Shared.Config is NULL!");
            
            Initialize.Do(Shared.Config.Api);
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}