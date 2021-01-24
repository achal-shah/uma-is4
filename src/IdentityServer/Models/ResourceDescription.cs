// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using System;
using System.Text.Json.Serialization;

namespace IdentityServer.UmaAs.Models
{
    public class ResourceDescription
    {
        [JsonPropertyName("resource_scopes")]
        public string[] ResourceScopes { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon_uri")]
        public string IconUri { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        public ResourceDescriptionDto ToDto(Guid id)
        {
            return new ResourceDescriptionDto
            {
                Id = id.ToString(),
                ResourceScopes = this.ResourceScopes,
                Description = this.Description,
                IconUri = this.IconUri,
                Name = this.Name,
                Type = this.Type
            };
        }
    }
}
