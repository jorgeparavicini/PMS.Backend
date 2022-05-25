using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Features.Reservation;
using PMS.Backend.Security;

namespace PMS.Backend.Service
{
    public class Startup
    {
        private const string CorsPolicy = "CorsPolicy";

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo { Title = "PMS.Backend.Service", Version = "v1" }
                    );
                }
            );

            //services.AddSecurity(Configuration);

            services.AddDbContext<PmsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PMS")));

            services.AddCors(
                options => options.AddPolicy(
                    CorsPolicy,
                    policy => policy.WithOrigins(Configuration["CorsOrigin"])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                )
            );

            // Auto Mapper
            services.AddAutoMapper(typeof(Features.Registrar).Assembly);

            // Custom Features
            //services.AddAuthenticationFeature();
            services.AddAPIFeature();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PMS.Backend.Service v1")
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
