using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using Services;
using Services.SearchLearning;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ElasticConfig(services);
            RedisConfig(services);

            services.AddTransient<ElasticService>();
            services.AddTransient<ElasticRepository>();
            services.AddTransient<SearchLearningService>();
        }

        private void RedisConfig(IServiceCollection services) {
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration["Redis:URL"];
                option.InstanceName = Configuration["Redis:InstanceName"];
            });
        }

        private void ElasticConfig(IServiceCollection services) {
            services.AddSingleton<IElasticClient>(new ElasticClient(
                new ConnectionSettings(new Uri(Configuration["Elasticsearch:URL"]))
                    .DefaultIndex(Configuration["Elasticsearch:Index"])
                    ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(b => b.AllowAnyOrigin());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
