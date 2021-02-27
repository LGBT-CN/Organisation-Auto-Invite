using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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