using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Profiles;
using LibraryApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace LibraryApi
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

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile(new BookProfile());
                // add additional profiles here...
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mapperConfiguration);

            services.AddDbContext<LibraryDataContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("library"));
            });

            // AddTransient - every time you create something that needs this thing, create a brand new instance.
            // AddScoped - only create ONE instance per HTTP Request
            services.AddScoped<IGetServerStatus, DrashtiServerStatus>();
            // AddSingleton - create one instance, and share it with everyone. NOTE: Must be thread safe.

            services.AddControllers();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Library API for BES 100",
                    Version = "1.0",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name="Jeff Gonzalez",
                        Email="jeff@hypertheory.com"
                    },
                    Description = "This is for Training"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API");
                c.RoutePrefix = "";
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
