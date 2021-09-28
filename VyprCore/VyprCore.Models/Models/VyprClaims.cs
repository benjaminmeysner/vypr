// <copyright file="VyprClaims.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Models.Models
{
    using VyprCore.Interfaces.Authorization;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Resources;
    using VyprCore.Utilities.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Vypr Claims/ Permissions.
    /// </summary>
    public class VyprClaims : IClaims
    {
        /// <summary>
        /// Convention is the following {Entity}_{Action}
        /// Actions consist of the following [Create, Read, Update, Delete, Administer]
        /// Standard text used to get permission description. For example "Can create Users"
        /// {Entity} will be looked up to get display value for user
        /// </summary>
        #region User
        [Display(ResourceType = typeof(StandardText), Name = "User_Create", Description = "User_Create")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string User_Create = "User_Create";

        [Display(ResourceType = typeof(StandardText), Name = "User_Update", Description = "User_Update")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string User_Update = "User_Update";

        [Display(ResourceType = typeof(StandardText), Name = "User_Read", Description = "User_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string User_Read = "User_Read";

        [Display(ResourceType = typeof(StandardText), Name = "User_Delete", Description = "User_Delete")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string User_Delete = "User_Delete";

        [Display(ResourceType = typeof(StandardText), Name = "User_SendInvitation", Description = "User_SendInvitation")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string User_SendInvitation = "User_SendInvitation";
        #endregion

        #region Tenant
        [Display(ResourceType = typeof(StandardText), Name = "Tenant_Create", Description = "Tenant_Create")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Tenant_Create = "Tenant_Create";

        [Display(ResourceType = typeof(StandardText), Name = "Tenant_Update", Description = "Tenant_Update")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Tenant_Update = "Tenant_Update";

        [Display(ResourceType = typeof(StandardText), Name = "Tenant_Read", Description = "Tenant_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Tenant_Read = "Tenant_Read";

        [Display(ResourceType = typeof(StandardText), Name = "Tenant_Delete", Description = "Tenant_Delete")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Tenant_Delete = "Tenant_Delete";
        #endregion

        #region Role
        [Display(ResourceType = typeof(StandardText), Name = "Role_Create", Description = "Role_Create")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Role_Create = "Role_Create";

        [Display(ResourceType = typeof(StandardText), Name = "Role_Update", Description = "Role_Update")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Role_Update = "Role_Update";

        [Display(ResourceType = typeof(StandardText), Name = "Role_Read", Description = "Role_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Role_Read = "Role_Read";

        [Display(ResourceType = typeof(StandardText), Name = "Role_Delete", Description = "Role_Delete")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Role_Delete = "Role_Delete";
        #endregion

        #region RoleClaims
        [Display(ResourceType = typeof(StandardText), Name = "RoleClaims_Administer", Description = "RoleClaims_Administer")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string RoleClaims_Administer = "RoleClaims_Administer";

        [Display(ResourceType = typeof(StandardText), Name = "RoleClaims_Read", Description = "RoleClaims_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string RoleClaims_Read = "RoleClaims_Read";
        #endregion

        #region TenantRoles
        [Display(ResourceType = typeof(StandardText), Name = "TenantRoles_Administer", Description = "TenantRoles_Administer")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string TenantRoles_Administer = "TenantRoles_Administer";

        [Display(ResourceType = typeof(StandardText), Name = "TenantRoles_Read", Description = "TenantRoles_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string TenantRoles_Read = "TenantRoles_Read";
        #endregion

        #region TenantUserRoles
        [Display(ResourceType = typeof(StandardText), Name = "TenantUserRoles_Administer", Description = "TenantUserRoles_Administer")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string TenantUserRoles_Administer = "TenantUserRoles_Administer";

        [Display(ResourceType = typeof(StandardText), Name = "TenantUserRoles_Read", Description = "TenantUserRoles_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string TenantUserRoles_Read = "TenantUserRoles_Read";
        #endregion

        #region UserTenant
        [Display(ResourceType = typeof(StandardText), Name = "UserTenant_Administer", Description = "UserTenant_Administer")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string UserTenant_Administer = "UserTenant_Administer";

        [Display(ResourceType = typeof(StandardText), Name = "UserTenant_Read", Description = "UserTenant_Read")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string UserTenant_Read = "UserTenant_Read";
        #endregion

        #region Swagger
        [Display(ResourceType = typeof(StandardText), Name = "Swagger_Administer", Description = "Swagger_Administer")]
        [Authorize(typeof(VyprSystemAdministrator))]
        public const string Swagger_Administer = "Swagger_Administer";
        #endregion

        public static IEnumerable<FieldInfo> GetAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IClaims).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .SelectMany(s => s.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(fi => fi.IsLiteral && !fi.IsInitOnly));
        }
    }
}
