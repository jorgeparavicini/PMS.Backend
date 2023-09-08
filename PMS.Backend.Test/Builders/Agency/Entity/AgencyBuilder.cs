using System;
using System.Collections.Generic;
using System.Linq;
using NuGet.Packaging;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Test.Builders.Agency.Entity;

public class AgencyBuilder
{
    private readonly IList<Action<AgencyContactBuilder>> _agencyContactBuilders =
        new List<Action<AgencyContactBuilder>>();

    private Guid _id;
    private string _legalName = "LegalName";
    private decimal? _defaultCommissionRate = 0m;
    private decimal? _defaultCommissionOnExtras = 0m;
    private CommissionMethod _commissionMethod = CommissionMethod.DeductedByAgency;
    private string? _emergencyPhone;
    private string? _emergencyEmail;

    public AgencyBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AgencyBuilder WithLegalName(string legalName)
    {
        _legalName = legalName;
        return this;
    }

    public AgencyBuilder WithDefaultCommissionRate(decimal? defaultCommissionRate)
    {
        _defaultCommissionRate = defaultCommissionRate;
        return this;
    }

    public AgencyBuilder WithDefaultCommissionOnExtras(decimal? defaultCommissionOnExtras)
    {
        _defaultCommissionOnExtras = defaultCommissionOnExtras;
        return this;
    }

    public AgencyBuilder WithCommissionMethod(CommissionMethod commissionMethod)
    {
        _commissionMethod = commissionMethod;
        return this;
    }

    public AgencyBuilder WithEmergencyPhone(string? emergencyPhone)
    {
        _emergencyPhone = emergencyPhone;
        return this;
    }

    public AgencyBuilder WithEmergencyEmail(string? emergencyEmail)
    {
        _emergencyEmail = emergencyEmail;
        return this;
    }

    public AgencyBuilder AddAgencyContacts(params Action<AgencyContactBuilder>[] agencyContactBuilders)
    {
        _agencyContactBuilders.AddRange(agencyContactBuilders);
        return this;
    }

    public Features.Agency.Models.Agency Build()
    {
        Features.Agency.Models.Agency agency = new()
        {
            Id = _id,
            LegalName = _legalName,
            DefaultCommissionRate = _defaultCommissionRate,
            DefaultCommissionOnExtras = _defaultCommissionOnExtras,
            CommissionMethod = _commissionMethod,
            EmergencyPhone = _emergencyPhone,
            EmergencyEmail = _emergencyEmail,
            AgencyContacts = null!,
        };

        agency.AgencyContacts = _agencyContactBuilders.Select(action =>
            {
                AgencyContactBuilder builder = new();
                action(builder);
                return builder.Build(agency, _id);
            })
            .ToList();

        return agency;
    }
}
