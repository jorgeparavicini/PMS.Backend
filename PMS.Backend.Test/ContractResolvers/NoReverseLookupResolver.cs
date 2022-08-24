using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PMS.Backend.Test.Utils;

namespace PMS.Backend.Test.ContractResolvers;

public class NoReverseLookupResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(
        Type type,
        MemberSerialization memberSerialization)
    {
        var props = base.CreateProperties(type, memberSerialization);
        return props.Where(p =>
            {
                var propertyInfo = type.GetProperty(p.PropertyName!)!;
                if (propertyInfo.CustomAttributes.Any(x =>
                        StaticData.IgnoredTypes.Contains(x.AttributeType)))
                {
                    return false;
                }

                return true;
            })
            .ToList();
    }
}
