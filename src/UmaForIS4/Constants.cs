using System;
using System.Collections.Generic;
using System.Text;

namespace Uma.IdentityServer4
{
    public static class EndpointNames
    {
        public const string UmaConfiguration = "UmaConfiguration";
    }

    public static class ProtocolRoutePaths
    {
        public const string ConnectPathPrefix = "connect";
        public const string ResourceSetPathPrefix = "rs";
        public const string UmaConfiguration = "/.well-known/uma-configuration";
        public const string Authorize = ConnectPathPrefix + "/authorize";
        public const string Token = ConnectPathPrefix + "/token";
        public const string Revocation = ConnectPathPrefix + "/revocation";
        public const string UserInfo = ConnectPathPrefix + "/userinfo";
        public const string Introspection = ConnectPathPrefix + "/introspect";
        public const string ResourceSetRegistration = ResourceSetPathPrefix + "/resourece_set";
        public const string PermissionRegistration = ConnectPathPrefix + "/permissionregister";
        public const string ResourceProtectionToken = ConnectPathPrefix + "/resourceprotectiontoken";
    }

    public static class UmaConfigurationConstants
    {
        public const string Version = "version";
        public const string PatProfilesSupported = "pat_profiles_supported";
        public const string AatProfilesSupported = "aat_profiles_supported";
        public const string RptProfilesSupported = "rpt_profiles_supported";
        public const string PatGrantTypesSupported = "pat_grant_types_supported";
        public const string AatGrantTypesSupported = "aat_grant_types_supported";
        public const string ResourceSetRegistrationEndpoint = "resource_set_registration_endpoint";
        public const string PermissionRegistrationEndpoint = "permission_registration_endpoint";
        public const string RptEndpoint = "rpt_endpoint";
    }

    /// <summary>
    /// Constants for local ProtectionApi access token authentication.
    /// </summary>
    public static class ProtectionApi
    {
        /// <summary>
        /// The authentication scheme when using the AddLocalApi helper.
        /// </summary>
        public const string AuthenticationScheme = "ProtectionApiToken";

        /// <summary>
        /// The API scope name when using the AddLocalApiAuthentication helper.
        /// </summary>
        public const string ScopeName = "ProtectionApi";

        /// <summary>
        /// The authorization policy name when using the AddLocalApiAuthentication helper.
        /// </summary>
        public const string PolicyName = AuthenticationScheme;
    }
}
