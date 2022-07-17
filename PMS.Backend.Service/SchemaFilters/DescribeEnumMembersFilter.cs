using System.Collections;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.SchemaFilters;

/// <summary>
/// Swagger schema filter to modify description of enum types so they show the XML docs attached
/// to each member of the enum.
/// </summary>
public class DescribeEnumMembersFilter : ISchemaFilter
{
    private readonly XDocument _xmlComments;
    private readonly string _assemblyName;

    public static string Prefix { get; } = "<p>Possible values:</p>";
    public static string Format { get; } = "<b>{0} - {1}</b>: {2}";

    /// <summary>
    /// Initializes a new instance of the <see cref="DescribeEnumMembersFilter"/> class.
    /// </summary>
    /// <param name="xmlComments">Document containing XML docs for enum members.</param>
    public DescribeEnumMembersFilter(XDocument xmlComments)
    {
        _xmlComments = xmlComments;
        _assemblyName = DetermineAssembly(xmlComments);
    }

    /// <summary>
    /// Apply this schema filter.
    /// </summary>
    /// <param name="schema">Target schema object.</param>
    /// <param name="context">Schema filter context.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        // Only process enums
        if (!type.IsEnum)
        {
            return;
        }

        if (type.Assembly.GetName().Name != _assemblyName)
        {
            return;
        }

        var sb = new StringBuilder(schema.Description);

        if (!string.IsNullOrEmpty(Prefix))
        {
            sb.AppendLine(Prefix);
        }

        sb.AppendLine("<ul>");
        foreach (var name in Enum.GetValues(type))
        {
            // Allows for large enums
            var value = Convert.ToInt64(name);
            var fullName = $"F:{type.FullName}.{name}";

            var description = _xmlComments.XPathEvaluate(
                $"normalize-space(//member[@name = '{fullName}']/summary/text())") as string;
            sb.AppendLine(string.Format($"<li>{Format}</li>", value, name, description));
        }

        sb.AppendLine("</ul>");
        schema.Description = sb.ToString();
    }

    private string DetermineAssembly(XDocument doc)
    {
        var name = ((IEnumerable)doc.XPathEvaluate("/doc/assembly")).Cast<XElement>()
            .ToList()
            .FirstOrDefault();
        return name!.Value;
    }
}
