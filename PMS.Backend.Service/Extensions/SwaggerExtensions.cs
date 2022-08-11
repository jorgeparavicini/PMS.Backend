﻿using System.Xml.Linq;
using System.Xml.XPath;
using PMS.Backend.Service.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Extensions;

/// <summary>
/// An extension class for swagger operations.
/// </summary>
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
}