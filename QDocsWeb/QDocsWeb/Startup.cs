using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QDocsWeb.Controllers.Services;
using QDocsWeb.Repositories;
using QDocsWeb.Services;
using QDocsWeb.Services.Repositories;

namespace QDocsWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServiceLayer(services);
            ConfigureDataLayer(services);
            
            services.AddMvc();

            services.AddCors(
                options => options.AddPolicy(
                    "AllowAll", p => p.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                    )
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAll");
            app.UseMvc();
        }

        public void ConfigureServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IQuestionService, QuestionService>();
        }

        public void ConfigureDataLayer(IServiceCollection services)
        {
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddScoped(x => new DocumentClient(
                new System.Uri(Configuration.GetValue<string>("DocumentDb:Endpoint")),
                Configuration.GetValue<string>("DocumentDb:ApiKey")));
        }
    }
}
