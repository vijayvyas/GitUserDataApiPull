using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleGitAppTest;
using StackExchange.Redis;
using System;
using System.Net;

namespace SampleAppTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();
            services.AddMemoryCache();
            services.AddTransient(typeof(RedisCacheService));
       
            services.AddSingleton<IUserApiService, UserApiService>();
            services.AddSingleton<IUserApiRepository, UserApiRepository>();
            services.AddTransient(typeof(CacheService));
            services.AddTransient(typeof(RedisCacheService));
            services.AddTransient<Func<ICacheService>>(serviceProvider => () =>
            {
                switch (Configuration.GetValue<string>("CacheType"))
                {
                    case "Redis":
                        return serviceProvider.GetService<RedisCacheService>();
                    default:
                        return serviceProvider.GetService<CacheService>();
                }
            });

            services.AddSingleton<IConnectionMultiplexer>(c => {

                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    EndPoints = { Configuration.GetValue<string>("redisConnection") }
                };
                return ConnectionMultiplexer.Connect(option);
                
                });
            services.Configure<CacheConfiguration>(Configuration.GetSection("CacheTimeConfiguration"));

            services.AddHttpClient("PublicGitApi", c => c.BaseAddress = new Uri(Configuration.GetValue<string>("GitUrl")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.ConfigExceptionHandler(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
