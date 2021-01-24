// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;

namespace ResourceServer.Models
{
    public class ResourceDescriptionViewModel
    {
        public ResourceDescriptionViewModel() { }

        public ResourceDescriptionViewModel(ResourceDescriptionDto rddto)
        {
            Id = rddto.Id;
            Scopes = string.Join(" ", rddto.ResourceScopes);
            Description = rddto.Description;
            IconUri = rddto.IconUri;
            Name = rddto.Name;
            ResourceType = rddto.Type;
        }

        public string Id { get; set; }

        /// <summary>
        /// Space separated.
        /// </summary>
        public string Scopes { get; set; }

        public string Description { get; set; }

        public string IconUri { get; set; }

        public string Name { get; set; }

        public string ResourceType { get; set; }
    }
}
