// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using IdentityServer.UmaAs.Contracts;
using IdentityServer.UmaAs.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Uma.IdentityServer4.ResponseHandling;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddUmaInMemoryResources(this IIdentityServerBuilder builder)
        {
            builder.Services.TryAddTransient<IUmaConfigurationResponseGenerator, UmaConfigurationResponseGenerator>();
            builder.Services.AddSingleton<IResourceDescriptionStore, InMemoryResourceDescriptionStore>();

            return builder;
        }
    }
}
