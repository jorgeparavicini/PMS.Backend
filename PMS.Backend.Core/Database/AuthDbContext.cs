// <copyright file="AuthDbContext.cs" company="Jorge Paravicini">
// Copyright (c) Jorge Paravicini. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Data;

namespace PMS.Backend.Core.Database
{
    /// <summary>
    /// Database context used for storing user information.
    /// Abstracted into own class to provide possibility of using multiple databases for application data and user data.
    /// </summary>
    public class AuthDbContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthDbContext"/> class.
        /// </summary>
        /// <param name="options">The db context options.</param>
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(Role.Roles);
        }
    }
}
