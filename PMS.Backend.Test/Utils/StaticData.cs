using Detached.Annotations;
using PMS.Backend.Core.Attributes;

namespace PMS.Backend.Test.Utils;

public static class StaticData
{
    public static readonly Type[] IgnoredTypes = new Type[]
        { typeof(ReverseLookupAttribute), typeof(AggregationAttribute) };
}
