using System;
using System.Collections.Generic;
using System.Linq;
using PMS.Backend.Domain.Common;

namespace PMS.Backend.Domain.ValueObjects;

public class CommissionMethod : Enumeration
{
    public static readonly CommissionMethod DeductedByAgency = new(1, nameof(DeductedByAgency).ToLowerInvariant());
    public static readonly CommissionMethod DeductedByProvider = new(2, nameof(DeductedByProvider).ToLowerInvariant());

    public CommissionMethod(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<CommissionMethod> List() => new[] { DeductedByAgency, DeductedByProvider };

    public static CommissionMethod FromName(string name)
    {
        CommissionMethod? state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        if (state == null)
        {
            throw new ArgumentException(
                $"Possible values for {nameof(CommissionMethod)}: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static CommissionMethod From(int id)
    {
        CommissionMethod? state = List().SingleOrDefault(s => s.Id == id);
        if (state == null)
        {
            throw new ArgumentException(
                $"Possible values for {nameof(CommissionMethod)}: {string.Join(",", List().Select(s => s.Id))}");
        }

        return state;
    }
}
