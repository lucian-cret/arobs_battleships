using Arobs_Battleships.Middleware;
using Arobs_Battleships.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Arobs_Battleships
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
            services.Configure<GridConfiguration>(o =>
            {
                var rowsAsString = Configuration.GetValue<string>("GridRows");
                var columnsAsString = Configuration.GetValue<string>("GridColumns");

                if (string.IsNullOrEmpty(rowsAsString) || string.IsNullOrEmpty(columnsAsString) ||
                    !int.TryParse(rowsAsString, out _) || !int.TryParse(columnsAsString, out _))
                {
                    throw new ConfigurationErrorsException("Missing value for grid configuration.");
                }
                o.Rows = int.Parse(rowsAsString);
                o.Columns = int.Parse(columnsAsString);
                if (o.Columns > 26)
                {
                    throw new ConfigurationErrorsException("26 is the maximum allowed number of columns.");
                }
            }); 
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandlingMiddleware();
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
