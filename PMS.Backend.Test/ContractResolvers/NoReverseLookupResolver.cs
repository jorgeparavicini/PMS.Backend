using Detached.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PMS.Backend.Core.Attributes;

namespace PMS.Backend.Test.ContractResolvers;

public class NoReverseLookupResolver : DefaultContractResolver
{
    private static readonly Type[] IgnoredTypes = new Type[]
        { typeof(ReverseLookupAttribute), typeof(AggregationAttribute) };

    protected override IList<JsonProperty> CreateProperties(
        Type type,
        MemberSerialization memberSerialization)
    {
        var props = base.CreateProperties(type, memberSerialization);
        return props.Where(p =>
            {
                var propertyInfo = type.GetProperty(p.PropertyName!)!;
                if (propertyInfo.CustomAttributes.Any(x => IgnoredTypes.Contains(x.AttributeType)))
                {
                    return false;
                }

                return true;
            })
            .ToList();
    }
}
