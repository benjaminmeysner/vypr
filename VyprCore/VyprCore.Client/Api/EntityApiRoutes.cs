// <copyright file="{MANY}.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Client.Api
{
    using VyprCore.Interfaces.Client;

    public class TenantApiRoute : IApiRoute
    {
        public string Route => "tenants";
    }

    public class RoleApiRoute : IApiRoute
    {
        public string Route => "roles";
    }

    public class RoleClaimsApiRoute : IApiRoute
    {
        public string Route => "roleclaims";
    }

    public class TenantRolesApiRoute : IApiRoute
    {
        public string Route => "tenantroles";
    }

    public class UserTenantApiRoute : IApiRoute
    {
        public string Route => "usertenants";
    }

    public class UserTenantRoleApiRoute : IApiRoute
    {
        public string Route => "usertenantroles";
    }

    public class ImageUtilitiesApiRoute : IApiRoute
    {
        public string Route => "utilities/image";
    }

    public class WebAuthnApiRoute : IApiRoute
    {
        public string Route => "webAuthn";
    }
}
