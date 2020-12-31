using IdentityModel;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uma.IdentityServer4.ResponseHandling
{
    public class UmaConfigurationResponseGenerator : IUmaConfigurationResponseGenerator
    {
        /// <summary>
        /// The options
        /// </summary>
        protected readonly IdentityServerOptions Options;

        /// <summary>
        /// The extension grants validator
        /// </summary>
        protected readonly ExtensionGrantValidator ExtensionGrants;

        /// <summary>
        /// The key material service
        /// </summary>
        protected readonly IKeyMaterialService Keys;

        /// <summary>
        /// The resource owner validator
        /// </summary>
        protected readonly IResourceOwnerPasswordValidator ResourceOwnerValidator;

        /// <summary>
        /// The resource store
        /// </summary>
        protected readonly IResourceStore ResourceStore;

        /// <summary>
        /// The secret parsers
        /// </summary>
        protected readonly ISecretsListParser SecretParsers;

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryResponseGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="resourceStore">The resource store.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="extensionGrants">The extension grants.</param>
        /// <param name="secretParsers">The secret parsers.</param>
        /// <param name="resourceOwnerValidator">The resource owner validator.</param>
        /// <param name="logger">The logger.</param>
        public UmaConfigurationResponseGenerator(
            IdentityServerOptions options,
            IResourceStore resourceStore,
            IKeyMaterialService keys,
            ExtensionGrantValidator extensionGrants,
            ISecretsListParser secretParsers,
            IResourceOwnerPasswordValidator resourceOwnerValidator,
            ILogger<DiscoveryResponseGenerator> logger)
        {
            Options = options;
            ResourceStore = resourceStore;
            Keys = keys;
            ExtensionGrants = extensionGrants;
            SecretParsers = secretParsers;
            ResourceOwnerValidator = resourceOwnerValidator;
            Logger = logger;
        }

        public virtual Task<Dictionary<string, object>> CreateConfigurationDocumentAsync(string baseUrl, string issuerUri)
        {
            var entries = new Dictionary<string, object>
            {
                { UmaConfigurationConstants.Version, "1.0"},
                { OidcConstants.Discovery.Issuer, issuerUri },
                { UmaConfigurationConstants.PatProfilesSupported, new string[]{ "bearer" } },
                { UmaConfigurationConstants.AatProfilesSupported, new string[]{ "bearer" } },
                { UmaConfigurationConstants.RptProfilesSupported, new string[]{ "https://docs.kantarainitiative.org/uma/profiles/uma-token-bearer-1.0" } },
                { UmaConfigurationConstants.PatGrantTypesSupported, new string[]{ "authorization_code"} },
                { UmaConfigurationConstants.AatGrantTypesSupported, new string[]{ "authorization_code"} },
                { OidcConstants.Discovery.TokenEndpoint, baseUrl + ProtocolRoutePaths.Token },
                { OidcConstants.Discovery.AuthorizationEndpoint, baseUrl + ProtocolRoutePaths.Authorize },
                { OidcConstants.Discovery.IntrospectionEndpoint, baseUrl + ProtocolRoutePaths.Introspection },
                { UmaConfigurationConstants.ResourceSetRegistrationEndpoint, baseUrl + ProtocolRoutePaths.ResourceSetRegistration },
                { UmaConfigurationConstants.PermissionRegistrationEndpoint, baseUrl + ProtocolRoutePaths.PermissionRegistration },
                { UmaConfigurationConstants.RptEndpoint, baseUrl + ProtocolRoutePaths.ResourceProtectionToken },
            };

            return Task.FromResult(entries);
        }
    }
}
