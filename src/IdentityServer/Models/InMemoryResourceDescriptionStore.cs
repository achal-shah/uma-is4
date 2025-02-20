﻿// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using IdentityServer.UmaAs.Contracts;
using System;
using System.Collections.Generic;

namespace IdentityServer.UmaAs.Models
{
    /// <summary>
    /// In memory implementation of the <see cref="IResourceDescriptionStore" /> interface.
    /// </summary>
    public class InMemoryResourceDescriptionStore : IResourceDescriptionStore
    {
        private readonly Dictionary<string, Dictionary<Guid, ResourceDescription>> resourceDescriptionsByUser;

        ///<inheritdoc/>
        public InMemoryResourceDescriptionStore()
        {
            resourceDescriptionsByUser = new Dictionary<string, Dictionary<Guid, ResourceDescription>>();
        }

        ///<inheritdoc/>
        public void AddDescription(string userId, Guid descriptionId, ResourceDescription resourceDescription)
        {
            if (!resourceDescriptionsByUser.ContainsKey(userId))
            {
                resourceDescriptionsByUser.Add(userId, new Dictionary<Guid, ResourceDescription>());
            }

            resourceDescriptionsByUser[userId].Add(descriptionId, resourceDescription);
        }

        ///<inheritdoc/>
        public bool DeleteDescription(string userId, Guid descriptionId)
        {
            if (resourceDescriptionsByUser.ContainsKey(userId))
            {
                return resourceDescriptionsByUser[userId].Remove(descriptionId);
            }

            return false;
        }

        ///<inheritdoc/>
        public ResourceDescription GetResourceDescription(string userId, Guid descriptionId)
        {
            if (!resourceDescriptionsByUser.ContainsKey(userId))
            {
                throw new ArgumentException($"User id: {userId} does not exist");
            }
            else
            {
                return resourceDescriptionsByUser[userId].GetValueOrDefault(descriptionId);
            }
        }

        ///<inheritdoc/>
        public IDictionary<Guid, ResourceDescription> GetResourceDescriptions(string userId)
        {
            if (!resourceDescriptionsByUser.ContainsKey(userId))
            {
                return null;
            }
            else
            {
                return resourceDescriptionsByUser[userId];
            }
        }
    }
}
