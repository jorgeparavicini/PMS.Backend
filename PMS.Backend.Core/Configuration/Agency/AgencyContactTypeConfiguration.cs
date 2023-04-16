// <copyright file="AgencyContactTypeConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Core.Configuration.Agency;

public class AgencyContactTypeConfiguration : EntityTypeConfiguration<AgencyContact>
{
    public override void Configure(EntityTypeBuilder<AgencyContact> builder)
    {
        base.Configure(builder);
    }
}
