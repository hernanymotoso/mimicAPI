using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mimiAPI.Config;
using mimiAPI.Interfaces;
using mimiAPI.Models;
using mimiAPI.Repositories;

namespace mimiAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region AutoMapper-Config

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DTOMapperProfileConfig());
                });
                IMapper mapper = config.CreateMapper();
                services.AddSingleton(mapper);

            #endregion

            /*Config data base*/
            services.AddDbContext<MimicContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                
            });
            
            services.AddMvc(options => options.EnableEndpointRouting = false );
            
            services.AddScoped<IPalavraRepository, PalavraRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            
            app.UseMvc();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }
    }
}