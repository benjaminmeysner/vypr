// <copyright file="VyprRoleManager.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Authentication.Managers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using VyprCore.Interfaces.Authorization;
    using VyprCore.Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Role Manager
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.RoleManager{Vypr.Server.Authentication.Classes.VyprRole}" />
    public class VyprRoleManager : RoleManager<VyprRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="roleValidators">The role validators.</param>
        /// <param name="lookupNormalizer">The lookup normalizer.</param>
        /// <param name="identityErrorDescriber">The identity error describer.</param>
        /// <param name="logger">The logger.</param>
        public VyprRoleManager(IRoleStore<VyprRole> store, IEnumerable<IRoleValidator<VyprRole>> roleValidators, ILookupNormalizer lookupNormalizer, IdentityErrorDescriber identityErrorDescriber, ILogger<RoleManager<VyprRole>> logger) : base(store, roleValidators, lookupNormalizer, identityErrorDescriber, logger)
        {
        }

        /// <summary>
        /// Gets the <see cref="T:Microsoft.Extensions.Logging.ILogger" /> used to log messages from the manager.
        /// </summary>
        /// <value>
        /// The <see cref="T:Microsoft.Extensions.Logging.ILogger" /> used to log messages from the manager.
        /// </value>
        public override ILogger Logger { get => base.Logger; set => base.Logger = value; }

        /// <summary>
        /// Gets an IQueryable collection of Roles if the persistence store is an <see cref="T:Microsoft.AspNetCore.Identity.IQueryableRoleStore`1" />,
        /// otherwise throws a <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <value>
        /// An IQueryable collection of Roles if the persistence store is an <see cref="T:Microsoft.AspNetCore.Identity.IQueryableRoleStore`1" />.
        /// </value>
        /// <remarks>
        /// Callers to this property should use <see cref="P:Microsoft.AspNetCore.Identity.RoleManager`1.SupportsQueryableRoles" /> to ensure the backing role store supports
        /// returning an IQueryable list of roles.
        /// </remarks>
        public override IQueryable<VyprRole> Roles => base.Roles;

        /// <summary>
        /// Gets a flag indicating whether the underlying persistence store supports returning an <see cref="T:System.Linq.IQueryable" /> collection of roles.
        /// </summary>
        /// <value>
        /// true if the underlying persistence store supports returning an <see cref="T:System.Linq.IQueryable" /> collection of roles, otherwise false.
        /// </value>
        public override bool SupportsQueryableRoles => base.SupportsQueryableRoles;

        /// <summary>
        /// Gets a flag indicating whether the underlying persistence store supports <see cref="T:System.Security.Claims.Claim" />s for roles.
        /// </summary>
        /// <value>
        /// true if the underlying persistence store supports <see cref="T:System.Security.Claims.Claim" />s for roles, otherwise false.
        /// </value>
        public override bool SupportsRoleClaims => base.SupportsRoleClaims;

        /// <summary>
        /// The cancellation token used to cancel operations.
        /// </summary>
        protected override CancellationToken CancellationToken => base.CancellationToken;

        /// <summary>
        /// Adds a claim to a role.
        /// </summary>
        /// <param name="role">The role to add the claim to.</param>
        /// <param name="claim">The claim to add.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> AddClaimAsync(VyprRole role, Claim claim)
        {
            return base.AddClaimAsync(role, claim);
        }

        /// <summary>
        /// Creates the specified <paramref name="role" /> in the persistence store.
        /// </summary>
        /// <param name="role">The role to create.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task<IdentityResult> CreateAsync(VyprRole role)
        {
            return base.CreateAsync(role);
        }

        /// <summary>
        /// Deletes the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role to delete.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> for the delete.
        /// </returns>
        public override Task<IdentityResult> DeleteAsync(VyprRole role)
        {
            return base.DeleteAsync(role);
        }

        /// <summary>
        /// Finds the role associated with the specified <paramref name="roleId" /> if any.
        /// </summary>
        /// <param name="roleId">The role ID whose role should be returned.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the role
        /// associated with the specified <paramref name="roleId" />
        /// </returns>
        public override Task<VyprRole> FindByIdAsync(string roleId)
        {
            return base.FindByIdAsync(roleId);
        }

        /// <summary>
        /// Finds the role associated with the specified <paramref name="roleName" /> if any.
        /// </summary>
        /// <param name="roleName">The name of the role to be returned.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the role
        /// associated with the specified <paramref name="roleName" />
        /// </returns>
        public override Task<VyprRole> FindByNameAsync(string roleName)
        {
            return base.FindByNameAsync(roleName);
        }

        /// <summary>
        /// Gets a list of claims associated with the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role whose claims should be returned.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the list of <see cref="T:System.Security.Claims.Claim" />s
        /// associated with the specified <paramref name="role" />.
        /// </returns>
        public async override Task<IList<Claim>> GetClaimsAsync(VyprRole role)
        {
            // wont be a unique list so changed it to do a distinct
            var claims = (await base.GetClaimsAsync(role)).Distinct().ToList();

            return claims;
        }

        /// <summary>
        /// Gets the ID of the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role whose ID should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the ID of the
        /// specified <paramref name="role" />.
        /// </returns>
        public override Task<string> GetRoleIdAsync(VyprRole role)
        {
            return base.GetRoleIdAsync(role);
        }

        /// <summary>
        /// Gets the name of the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role whose name should be retrieved.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the name of the
        /// specified <paramref name="role" />.
        /// </returns>
        public override Task<string> GetRoleNameAsync(VyprRole role)
        {
            return base.GetRoleNameAsync(role);
        }

        /// <summary>
        /// Gets a normalized representation of the specified <paramref name="key" />.
        /// </summary>
        /// <param name="key">The value to normalize.</param>
        /// <returns>
        /// A normalized representation of the specified <paramref name="key" />.
        /// </returns>
        public override string NormalizeKey(string key)
        {
            return base.NormalizeKey(key);
        }

        /// <summary>
        /// Removes a claim from a role.
        /// </summary>
        /// <param name="role">The role to remove the claim from.</param>
        /// <param name="claim">The claim to remove.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> RemoveClaimAsync(VyprRole role, Claim claim)
        {
            return base.RemoveClaimAsync(role, claim);
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="roleName" /> exists.
        /// </summary>
        /// <param name="roleName">The role name whose existence should be checked.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing true if the role name exists, otherwise false.
        /// </returns>
        public override Task<bool> RoleExistsAsync(string roleName)
        {
            return base.RoleExistsAsync(roleName);
        }

        /// <summary>
        /// Sets the name of the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role whose name should be set.</param>
        /// <param name="name">The name to set.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        public override Task<IdentityResult> SetRoleNameAsync(VyprRole role, string name)
        {
            return base.SetRoleNameAsync(role, name);
        }

        /// <summary>
        /// Updates the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role to updated.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> for the update.
        /// </returns>
        public override Task<IdentityResult> UpdateAsync(VyprRole role)
        {
            return base.UpdateAsync(role);
        }

        /// <summary>
        /// Updates the normalized name for the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role whose normalized name needs to be updated.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task UpdateNormalizedRoleNameAsync(VyprRole role)
        {
            return base.UpdateNormalizedRoleNameAsync(role);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the role manager and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Called to update the role after validating and updating the normalized role name.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>
        /// Whether the operation was successful.
        /// </returns>
        protected override Task<IdentityResult> UpdateRoleAsync(VyprRole role)
        {
            return base.UpdateRoleAsync(role);
        }

        /// <summary>
        /// Should return <see cref="P:Microsoft.AspNetCore.Identity.IdentityResult.Success" /> if validation is successful. This is
        /// called before saving the role via Create or Update.
        /// </summary>
        /// <param name="role">The role</param>
        /// <returns>
        /// A <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> representing whether validation was successful.
        /// </returns>
        protected override Task<IdentityResult> ValidateRoleAsync(VyprRole role)
        {
            return base.ValidateRoleAsync(role);
        }

        /// <summary>
        /// Gets the role types.
        /// </summary>
        /// <returns>list of role types instantiated</returns>
        public virtual List<IRoleType> GetRoleTypes()
        {
            var type = typeof(IRoleType);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            List<IRoleType> instantiatedTypes = types.Select(s => (IRoleType)Activator.CreateInstance(s)).ToList();

            return instantiatedTypes;
        }
    }
}
