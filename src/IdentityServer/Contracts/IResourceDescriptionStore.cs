// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using IdentityServer.UmaAs.Models;
using System;

namespace IdentityServer.UmaAs.Contracts
{
    public interface IResourceDescriptionStore
    {
        void AddDescription(string userId, Guid descriptionId, ResourceDescription resourceDescription);

        ResourceDescription GetResourceDescription(string userId, Guid descriptionId);
    }
}
