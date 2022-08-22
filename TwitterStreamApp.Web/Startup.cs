using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Twitter.StreamApp.Cache;
using Twitter.StreamApp.Data;
using Twitter.StreamApp.Stream.Stream;
using Twitter.StreamApp.Stream.Stream.Base.Subscription;
using Twitter.StreamApp.Stream.Stream.Interfaces;
using Twitter.StreamApp.Stream.Stream.Options;
using Twitter.StreamApp.Web.Config.Extensions;

namespace Twitter.StreamApp.Web
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
            services.RegisterConfigSettings(Configuration);

            services.AddSingleton<ISubscriptionService<Tweet>, SubscriptionService<Tweet>>();
            services.AddSingleton<ISimpleCacheCalcService, SimpleCacheCalcService>();

            services.AddSingleton<ISampleStreamOptions, SampleStreamOptions>()
                    .AddSingleton<ICustomRequestOptions>(sp => sp.GetRequiredService<SampleStreamOptions>());

            services.AddSingleton<IFilteredStreamOptions, FilteredStreamOptions>()
                    .AddSingleton<ICustomRequestOptions>(sp => sp.GetRequiredService<FilteredStreamOptions>());

            services.AddSingleton<IStreamService, SampledStreamService>();
            services.AddSingleton<IStreamService, FilteredStreamService>();
            services.AddSingleton<IStreamServiceManager, StreamServiceManager>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TwitterStreamApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app
            , IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwitterStreamApp v1"));
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
