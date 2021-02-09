using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.IO;

namespace FennasPrem.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "production")
            {
                var options = ConfigurationOptions.Parse($"{Environment.GetEnvironmentVariable("REDIS_HOST")}:6379");
                options.Password = $"{ Environment.GetEnvironmentVariable("REDIS_HOST_PASSWORD")}";

                var redis = ConnectionMultiplexer.Connect(options);

                services.AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
                    .SetApplicationName("fennasprem");
            }
            services.AddOrchardCms();

        }
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOrchardCore();

        }
    }
}
