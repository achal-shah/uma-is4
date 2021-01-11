// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using IdentityServer.UmaAs.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer.UmaAs.Contracts
{
    /// <summary>
    /// Intefrace for a ResourceDescription store.
    /// The description records are organized by user, using their "sub" claim.
    /// </summary>
    public interface IResourceDescriptionStore
    {
        /// <summary>
        /// Adds a resource description record.
        /// </summary>
        /// <param name="userId">The resource owner.</param>
        /// <param name="descriptionId">The unique resource description id.</param>
        /// <param name="resourceDescription">The resource description being added.</param>
        void AddDescription(string userId, Guid descriptionId, ResourceDescription resourceDescription);

        /// <summary>
        /// Deletes a resource description record.
        /// </summary>
        /// <param name="userId">The resource owner.</param>
        /// <param name="descriptionId">The description id to be deleted.</param>
        /// <returns>True if the description was deleted and false if it does not exist.</returns>
        bool DeleteDescription(string userId, Guid descriptionId);

        /// <summary>
        /// Returns a list of description ids.
        /// </summary>
        /// <param name="userId">The resource owner of the descriptions.</param>
        /// <returns>A dictionary of resource description ids and resource description records.</returns>
        IDictionary<Guid, ResourceDescription> GetResourceDescriptions(string userId);

        /// <summary>
        /// Gets a single resource description.
        /// </summary>
        /// <param name="userId">The resource owner id.</param>
        /// <param name="descriptionId">The description id.</param>
        /// <returns>The resource description record.</returns>
        ResourceDescription GetResourceDescription(string userId, Guid descriptionId);
    }
}
