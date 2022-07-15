using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service;

/// <summary>
/// The parent class for the entry point of the service.
/// </summary>
public static class Program
{
    /// <summary>
    /// The entry point to start the web service
    /// </summary>
    /// <param name="args">Optional command line arguments</param>
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var CorsPolicy = "Cors";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CorsPolicy,
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        // Add Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "PMS.Backend.Service", Version = "v1" });

            AddXmlDocs(c);
            c.SupportNonNullableReferenceTypes();
        });


        // Add Database
        builder.Services.AddDbContext<PmsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PMS")));

        builder.Services.AddAutoMapper(typeof(Registrar).Assembly);
        builder.Services.AddAPI();

        var app = builder.Build();
        app.UsePathBase(new PathString("/api"));

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.UseCors(CorsPolicy);
        app.MapControllers();
        app.Run();
    }

    private static void AddXmlDocs(SwaggerGenOptions options)
    {
        // Generate paths for the XML doc files in the assembly's directory.
        var xmlDocPaths = Directory.GetFiles(
            path: AppDomain.CurrentDomain.BaseDirectory,
            searchPattern: "*.xml");

        // Load the XML docs for processing.
        var xmlDocs = (from docPath in xmlDocPaths select XDocument.Load(docPath))
            .ToList();

        // Need a map for looking up member elements by name.
        var targetMemberElements = new Dictionary<string, XElement>();

        // Add member elements across all XML docs to the look-up table. We want <member> elements
        // that have a 'name' attribute but don't contain an <inheritdoc> child element.
        foreach (var doc in xmlDocs)
        {
            var members = doc.XPathSelectElements("/doc/members/member[@name and not(inheritdoc)]");

            foreach (var member in members)
            {
                targetMemberElements.Add(member.Attribute("name")!.Value, member);
            }
        }

        // For each <member> element that has an <inheritdoc> child element which references another
        // <member> element, replace the <inheritdoc> element with the nodes of the referenced
        // <member> element (effectively this 'dereferences the pointer' which is something
        // Swagger doesn't support).
        foreach (var doc in xmlDocs)
        {
            var pointerMembers = doc.XPathSelectElements("/doc/members/member/*[inheritdoc[@cref]]");

            foreach (var pointerMember in pointerMembers)
            {
                var pointerElement = pointerMember.Element("inheritdoc");
                var targetMemberName = pointerElement!.Attribute("cref")!.Value;

                if (targetMemberElements.TryGetValue(targetMemberName, out var targetMember))
                {
                    pointerElement.ReplaceWith(targetMember.Nodes());
                }
            }
        }

        // Replace all <see> elements with the unqualified member name that they point to
        // (Swagger uses the fully qualified name which makes no sense because the relevant classes
        // and namespaces are not useful when calling an API over HTTP).
        foreach (var doc in xmlDocs)
        {
            foreach (var seeElement in doc.XPathSelectElements("//see[@cref]"))
            {
                var targetMemberName = seeElement.Attribute("cref")!.Value;
                var shortMemberName =
                    targetMemberName.Substring(targetMemberName.LastIndexOf('.') + 1);

                if (targetMemberName.StartsWith("M:")) shortMemberName += "()";

                seeElement.ReplaceWith(shortMemberName);
            }
        }

        // Add pre-processed XML docs to Swagger.
        foreach (var doc in xmlDocs)
        {
            options.IncludeXmlComments(() => new XPathDocument(doc.CreateReader()), true);
        }
    }
}
