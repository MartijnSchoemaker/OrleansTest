using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using OrleansTest.Grains;

namespace OrleansTest
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
                    webBuilder.UseStartup<Startup>();
                })
                .UseOrleans(builder =>
                {
                    var storageConnectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING");
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    var isDevelopment = environment == Environments.Development;

                    if (isDevelopment)
                    {
                        builder.UseLocalhostClustering();
                    }
                    else
                    {
                        builder.UseKubernetesHosting();
                        builder.UseAzureStorageClustering(options => options.ConnectionString = storageConnectionString);
                        builder.ConfigureEndpoints(new Random(1).Next(10001, 10100), new Random(1).Next(20001, 20100));
                    }

                    builder.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(BasketGrain).Assembly).WithReferences());
                    builder.AddMemoryGrainStorage("OrleansStorage");
                    builder.AddAzureTableGrainStorageAsDefault(options =>
                    {
                        options.UseJson = true;
                        options.ConnectionString = storageConnectionString;
                    });
                });
    }


}
