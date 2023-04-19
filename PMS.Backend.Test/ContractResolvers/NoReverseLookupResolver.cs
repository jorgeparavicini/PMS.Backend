using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PMS.Backend.Test.ContractResolvers;

public class NoReverseLookupResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(
        Type type,
        MemberSerialization memberSerialization)
    {
        return new List<JsonProperty>();
        /*var props = base.CreateProperties(type, memberSerialization);
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
            .ToList();*/
    }
}
