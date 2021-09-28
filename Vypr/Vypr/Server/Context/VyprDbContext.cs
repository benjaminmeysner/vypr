// <copyright file="VyprDbContext.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data
{
    using IdentityServer4.EntityFramework.Entities;
    using IdentityServer4.EntityFramework.Extensions;
    using IdentityServer4.EntityFramework.Interfaces;
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using VyprCore.Models.Domain;
    using System.Threading.Tasks;
    using Vypr.Server.Extensions;

    /// <summary>
    /// Vypr DbContext
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Vypr.Server.Authentication.Classes.VyprUser, Vypr.Server.Authentication.Classes.VyprRole, System.Int32}" />
    /// <seealso cref="IdentityServer4.EntityFramework.Interfaces.IPersistedGrantDbContext" />
    public class VyprDbContext : IdentityDbContext<VyprUser, VyprRole, int, VyprUserClaim, VyprUserRole, VyprUserLogin, VyprRoleClaim, VyprUserToken>, IPersistedGrantDbContext
    {
        /// <summary>
        /// The operational store options
        /// </summary>
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        /// <summary>
        /// Gets or sets the device flow codes.
        /// </summary>
        /// <value>
        /// The device flow codes.
        /// </value>
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        /// <summary>
        /// Gets or sets the persisted grants.
        /// </summary>
        /// <value>
        /// The persisted grants.
        /// </value>
        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        /// <summary>
        /// Gets or sets the system administrators.
        /// </summary>
        /// <value>
        /// The system administrators.
        /// </value>
        public DbSet<VyprSystemAdministrator> SystemAdministrators { get; set; }

        /// <summary>
        /// Gets or sets the web auth n credentials.
        /// </summary>
        /// <value>
        /// The web auth n creds.
        /// </value>
        public DbSet<VyprWebAuthnCredential> WebAuthnCredentials { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="operationalStoreOptions">The operational store options.</param>
        public VyprDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
            Database.Migrate();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
            BuildCustomEntities(builder);
            builder.SeedSystemAdministrator();
        }

        /// <summary>
        /// Builds the identity entities.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public virtual void BuildCustomEntities(ModelBuilder builder)
        {
            builder.Ignore<IdentityUserRole<int>>();
            builder.Entity<VyprUser>().ToTable("VyprUsers");
            builder.Entity<VyprUserLogin>().Ignore(x => x.Id).ToTable("VyprUserLogins").HasKey(x => x.UserLoginId);
            builder.Entity<VyprUserClaim>().ToTable("VyprUserClaims");
            builder.Entity<VyprUserToken>().Ignore(x => x.Id).ToTable("VyprUserTokens").HasKey(x => x.UserTokenId);
            builder.Entity<VyprRole>().ToTable("VyprRoles");
            builder.Entity<VyprUserRole>().ToTable("VyprUserRole").HasKey(x => x.UserRoleId);
            builder.Entity<VyprRoleClaim>().ToTable("VyprRolesClaims");

            builder.Entity<VyprRoleClaim>().HasOne(s => s.Role).WithMany(s => s.RoleClaims).HasForeignKey(s => s.RoleId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<VyprUserClaim>().HasOne(s => s.User).WithMany(s => s.UserClaims).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<VyprUserToken>().HasOne(s => s.User).WithMany(s => s.UserTokens).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<VyprUserLogin>().HasOne(s => s.User).WithMany(s => s.UserLogins).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<VyprUserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VyprSystemAdministrator>().HasKey(x => x.UserId);
            builder.Entity<VyprSystemAdministrator>().HasOne<VyprUser>().WithOne(x => x.SystemAdministrator).HasForeignKey<VyprSystemAdministrator>(s => s.UserId);
            builder.Entity<VyprWebAuthnCredential>().HasKey(m => m.Id);
        }
    }
}
