// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using ResourceServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceServer.Contracts
{
    /// <summary>
    /// Client to interact with the Protection API at the authorization server.
    /// </summary>
    public interface IProtectionApiClient
    {
        /// <summary>
        /// Invoke the create resource description API.
        /// </summary>
        /// <param name="resourceDescription">The resource description.</param>
        /// <param name="accessToken">The access token to access the protection API.</param>
        /// <returns>The creation response.</returns>
        Task<CreateResourceDescriptionResponse> CreateResourceDescriptionAsync(ResourceDescriptionModel resourceDescription, string accessToken);

        /// <summary>
        /// Returns the resource descriptions for the subject in the access token.
        /// </summary>
        /// <param name="accessToken">The access token to access the protection API.</param>
        /// <returns>The resource descriptions.</returns>
        Task<IList<ResourceDescriptionDto>> GetResourceDescriptionsAsync(string accessToken);
    }
}
