// <copyright file="VyprRoleStore.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Authentication.Stores
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using VyprCore.Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using VyprCore.Foundation.Context;

    /// <summary>
    /// Vypr Role Store
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.RoleStore{VyprCore.Foundation.Authentication.Classes.VyprRole, VyprCore.Foundation.DbContext.VyprDbContext, System.Int32, VyprCore.Foundation.Authentication.Classes.VyprUserTenantRoles, VyprCore.Foundation.Authentication.Classes.VyprRoleClaims}" />
    public class VyprRoleStore : RoleStore<VyprRole, VyprDbContext, int, VyprUserRole, VyprRoleClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprRoleStore"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public VyprRoleStore(VyprDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the database context for this store.
        /// </summary>
        public override VyprDbContext Context => base.Context;

        /// <summary>
        /// A navigation property for the roles the store contains.
        /// </summary>
        public override IQueryable<VyprRole> Roles => base.Roles;

        /// <summary>
        /// Adds the <paramref name="claim" /> given to the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role to add the claim to.</param>
        /// <param name="claim">The claim to add to the role.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task AddClaimAsync(VyprRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            return base.AddClaimAsync(role, claim, cancellationToken);
        }

        /// <summary>
        /// Converts the provided <paramref name="id" /> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>
        /// An instance of <typeparamref name="TKey" /> representing the provided <paramref name="id" />.
        /// </returns>
        public override int ConvertIdFromString(string id)
        {
            return base.ConvertIdFromString(id);
        }

        /// <summary>
        /// Converts the provided <paramref name="id" /> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>
        /// An <see cref="T:System.String" /> representation of the provided <paramref name="id" />.
        /// </returns>
        public override string ConvertIdToString(int id)
        {
            return base.ConvertIdToString(id);
        }

        /// <summary>
        /// Creates a new role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to create in the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.
        /// </returns>
        public override Task<IdentityResult> CreateAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.CreateAsync(role, cancellationToken);
        }

        /// <summary>
        /// Deletes a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to delete from the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.
        /// </returns>
        public override Task<IdentityResult> DeleteAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(role, cancellationToken);
        }

        /// <summary>
        /// Finds the role who has the specified ID as an asynchronous operation.
        /// </summary>
        /// <param name="id">The role ID to look for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.
        /// </returns>
        public override Task<VyprRole> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return base.FindByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="normalizedName">The normalized role name to look for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that result of the look up.
        /// </returns>
        public override Task<VyprRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
        {
            return base.FindByNameAsync(normalizedName, cancellationToken);
        }

        /// <summary>
        /// Get the claims associated with the specified <paramref name="role" /> as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose claims should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the claims granted to a role.
        /// </returns>
        public override Task<IList<Claim>> GetClaimsAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.GetClaimsAsync(role, cancellationToken);
        }

        /// <summary>
        /// Get a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.
        /// </returns>
        public override Task<string> GetNormalizedRoleNameAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.GetNormalizedRoleNameAsync(role, cancellationToken);
        }

        /// <summary>
        /// Gets the ID for a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose ID should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the ID of the role.
        /// </returns>
        public override Task<string> GetRoleIdAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.GetRoleIdAsync(role, cancellationToken);
        }

        /// <summary>
        /// Gets the name of a role from the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that contains the name of the role.
        /// </returns>
        public override Task<string> GetRoleNameAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.GetRoleNameAsync(role, cancellationToken);
        }

        /// <summary>
        /// Removes the <paramref name="claim" /> given from the specified <paramref name="role" />.
        /// </summary>
        /// <param name="role">The role to remove the claim from.</param>
        /// <param name="claim">The claim to remove from the role.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task RemoveClaimAsync(VyprRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            return base.RemoveClaimAsync(role, claim, cancellationToken);
        }

        /// <summary>
        /// Set a role's normalized name as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose normalized name should be set.</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetNormalizedRoleNameAsync(VyprRole role, string normalizedName, CancellationToken cancellationToken = default)
        {
            return base.SetNormalizedRoleNameAsync(role, normalizedName, cancellationToken);
        }

        /// <summary>
        /// Sets the name of a role in the store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role whose name should be set.</param>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
        /// </returns>
        public override Task SetRoleNameAsync(VyprRole role, string roleName, CancellationToken cancellationToken = default)
        {
            return base.SetRoleNameAsync(role, roleName, cancellationToken);
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to update in the store.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the asynchronous query.
        /// </returns>
        public override Task<IdentityResult> UpdateAsync(VyprRole role, CancellationToken cancellationToken = default)
        {
            return base.UpdateAsync(role, cancellationToken);
        }

        /// <summary>
        /// Creates an entity representing a role claim.
        /// </summary>
        /// <param name="role">The associated role.</param>
        /// <param name="claim">The associated claim.</param>
        /// <returns>
        /// The role claim entity.
        /// </returns>
        protected override VyprRoleClaim CreateRoleClaim(VyprRole role, Claim claim)
        {
            return base.CreateRoleClaim(role, claim);
        }
    }
}
