using System;
using System.Collections.Generic;
using System.Linq;
using NuGet.Packaging;
using PMS.Backend.Core.Domain.Models;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class CreateAgencyWithContactsAgencyInputBuilder
{
    private readonly IList<Action<CreateAgencyWithContactsAgencyContactInputBuilder>> _agencyContactBuilders =
        new List<Action<CreateAgencyWithContactsAgencyContactInputBuilder>>();

    private string _legalName = "LegalName";
    private decimal? _defaultCommissionRate = 0m;
    private decimal? _defaultCommissionOnExtras = 0m;
    private CommissionMethod _commissionMethod = CommissionMethod.DeductedByAgency;
    private string? _emergencyPhone;
    private string? _emergencyEmail;

    public CreateAgencyWithContactsAgencyInputBuilder WithLegalName(string legalName)
    {
        _legalName = legalName;
        return this;
    }

    public CreateAgencyWithContactsAgencyInputBuilder WithDefaultCommissionRate(decimal? defaultCommissionRate)
    {
        _defaultCommissionRate = defaultCommissionRate;
        return this;
    }

    public CreateAgencyWithContactsAgencyInputBuilder WithDefaultCommissionOnExtras(decimal? defaultCommissionOnExtras)
    {
        _defaultCommissionOnExtras = defaultCommissionOnExtras;
        return this;
    }

    public CreateAgencyWithContactsAgencyInputBuilder WithCommissionMethod(CommissionMethod commissionMethod)
    {
        _commissionMethod = commissionMethod;
        return this;
    }

    public CreateAgencyWithContactsAgencyInputBuilder WithEmergencyPhone(string? emergencyPhone)
    {
        _emergencyPhone = emergencyPhone;
        return this;
    }

    public CreateAgencyWithContactsAgencyInputBuilder WithEmergencyEmail(string? emergencyEmail)
    {
        _emergencyEmail = emergencyEmail;
        return this;
    }

    public CreateAgencyWithContactsAgencyInputBuilder AddAgencyContacts(
        params Action<CreateAgencyWithContactsAgencyContactInputBuilder>[] agencyContactBuilders)
    {
        _agencyContactBuilders.AddRange(agencyContactBuilders);
        return this;
    }

    public CreateAgencyWithContactsAgencyInput Build()
    {
        CreateAgencyWithContactsAgencyInput agency = new()
        {
            LegalName = _legalName,
            DefaultCommissionRate = _defaultCommissionRate,
            DefaultCommissionOnExtras = _defaultCommissionOnExtras,
            CommissionMethod = _commissionMethod,
            EmergencyPhone = _emergencyPhone,
            EmergencyEmail = _emergencyEmail,
            AgencyContacts = _agencyContactBuilders.Select(action =>
                {
                    CreateAgencyWithContactsAgencyContactInputBuilder builder = new();
                    action(builder);
                    return builder.Build();
                })
                .ToList(),
        };

        return agency;
    }
}
