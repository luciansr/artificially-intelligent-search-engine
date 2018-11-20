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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using Services;

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
            //ElasticConfig(services);


            var url = Configuration["ElasticSettings:URL"];
            var defaultIndex = Configuration["ElasticSettings:Index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                // .DefaultMappingFor<Post>(m => m
                //     .Ignore(p => p.IsPublished)
                //     .PropertyName(p => p.ID, "id")
                // )
                // .DefaultMappingFor<Comment>(m => m
                //     .Ignore(c => c.Email)
                //     .Ignore(c => c.IsAdmin)
                //     .PropertyName(c => c.ID, "id")
                // )
            ;

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));

            services.AddTransient<ElasticService>();
            services.AddTransient<ElasticRepository>();
        }

        private void ElasticConfig(IServiceCollection services) {
            var url = Configuration["Elasticsearch:URL"];
            var defaultIndex = Configuration["Elasticsearch:Index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                // .DefaultMappingFor<Post>(m => m
                //     .Ignore(p => p.IsPublished)
                //     .PropertyName(p => p.ID, "id")
                // )
                // .DefaultMappingFor<Comment>(m => m
                //     .Ignore(c => c.Email)
                //     .Ignore(c => c.IsAdmin)
                //     .PropertyName(c => c.ID, "id")
                // )
            ;

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
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
