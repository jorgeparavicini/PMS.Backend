using System;
using PMS.Backend.Core.Domain.Models;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class EditAgencyInputBuilder
{
    private Guid _id;
    private string _legalName = "LegalName";
    private decimal? _defaultCommissionRate = 0m;
    private decimal? _defaultCommissionOnExtras = 0m;
    private CommissionMethod _commissionMethod = CommissionMethod.DeductedByAgency;
    private string? _emergencyPhone;
    private string? _emergencyEmail;

    public EditAgencyInputBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public EditAgencyInputBuilder WithLegalName(string legalName)
    {
        _legalName = legalName;
        return this;
    }

    public EditAgencyInputBuilder WithDefaultCommissionRate(decimal? defaultCommissionRate)
    {
        _defaultCommissionRate = defaultCommissionRate;
        return this;
    }

    public EditAgencyInputBuilder WithDefaultCommissionOnExtras(decimal? defaultCommissionOnExtras)
    {
        _defaultCommissionOnExtras = defaultCommissionOnExtras;
        return this;
    }

    public EditAgencyInputBuilder WithCommissionMethod(CommissionMethod commissionMethod)
    {
        _commissionMethod = commissionMethod;
        return this;
    }

    public EditAgencyInputBuilder WithEmergencyPhone(string? emergencyPhone)
    {
        _emergencyPhone = emergencyPhone;
        return this;
    }

    public EditAgencyInputBuilder WithEmergencyEmail(string? emergencyEmail)
    {
        _emergencyEmail = emergencyEmail;
        return this;
    }

    public EditAgencyInput Build()
    {
        return new EditAgencyInput
        {
            Id = _id,
            LegalName = _legalName,
            DefaultCommissionRate = _defaultCommissionRate,
            DefaultCommissionOnExtras = _defaultCommissionOnExtras,
            CommissionMethod = _commissionMethod,
            EmergencyPhone = _emergencyPhone,
            EmergencyEmail = _emergencyEmail,
        };
    }
}
