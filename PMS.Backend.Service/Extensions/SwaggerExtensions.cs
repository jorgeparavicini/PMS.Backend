using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PMS.Backend.Common.Security;
using PMS.Backend.Service.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Extensions;

/// <summary>
/// An extension class for swagger operations.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    /// <summary>
    /// Add inheritdoc and see XML tags to swagger documentation.
    /// </summary>
    /// <param name="options">The swagger generation options which should be modified.</param>
    public static void AddXmlDocs(this SwaggerGenOptions options)
    {
        // Generate paths for the XML doc files in the assembly's directory.
        var xmlDocPaths = Directory.GetFiles(
            path: AppDomain.CurrentDomain.BaseDirectory,
            searchPattern: "*.xml");

        // Load the XML docs for processing.
        var xmlDocs = xmlDocPaths.Select(XDocument.Load);

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
            var pointerMembers =
                doc.XPathSelectElements("/doc/members/member/*[inheritdoc[@cref]]");

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
            options.SchemaFilter<DescribeEnumMembersFilter>(doc);
        }
    }

    /// <summary>
    /// Adds the auth0 security scheme to a <see cref="SwaggerGenOptions"/>.
    /// </summary>
    /// <param name="options">The swagger options to modify.</param>
    /// <param name="domain">The auth0 domain.</param>
    /// <param name="audience">The auth0 audience.</param>
    public static void AddSecurityScheme(
        this SwaggerGenOptions options,
        string domain,
        string audience)
    {
        var docPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory,
            Assembly.GetAssembly(typeof(Policy))!.GetName().Name) + ".xml";

        XmlDocument? doc = null;
        if (File.Exists(docPath))
        {
            doc = new XmlDocument();
            doc.Load(docPath);
        }

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{domain}authorize?audience={audience}"),
                }
            }
        };

        options.AddSecurityDefinition("Bearer", securityScheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, new[] { "Bearer" } }
        });
    }
}
