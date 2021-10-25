using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;

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
            services.AddTransient(typeof(RedisCacheService<>));
       
            services.AddSingleton<IUserApiService, UserApiServiceImpl>();
            services.AddSingleton<IUserApiRepository, UserApiRepository>();
            services.AddSingleton(typeof(ICacheService<>), typeof(RedisCacheService<>));
            services.AddSingleton<IConnectionMultiplexer>(c => {

                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    EndPoints = { Configuration.GetValue<string>("redisConnection") }
                };
                return ConnectionMultiplexer.Connect(option);
                
                });

            services.AddHttpClient("PublicGitApi", c => c.BaseAddress = new Uri("https://api.github.com/"));

            services.AddHttpClient("PublicHolidaysApi", c => c.BaseAddress = new Uri("https://date.nager.at"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
