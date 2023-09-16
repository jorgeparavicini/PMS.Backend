using System;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace PMS.Backend.Api.Conventions;

public class CommandNamingConvention : DefaultNamingConventions
{
    public override string GetTypeName(Type type, TypeKind kind)
    {
        if (kind == TypeKind.InputObject && type.Name.EndsWith("Command"))
        {
            return type.Name.Replace("Command", "Input");
        }

        return base.GetTypeName(type, kind);
    }
}
