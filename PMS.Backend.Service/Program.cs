using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;

namespace PMS.Backend.Service;

public static class Program
{
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        // Add Swagger
        builder.Services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "PMS.Backend.Service", Version = "v1" }));

        // Add Database
        builder.Services.AddDbContext<PmsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PMS")));

        builder.Services.AddAutoMapper(typeof(Registrar).Assembly);
        builder.Services.AddAPI();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}