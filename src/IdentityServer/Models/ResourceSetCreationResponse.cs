// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json.Serialization;

namespace IdentityServer.UmaAs.Models
{
    public class ResourceSetCreationResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("user_access_policy_uri")]
        public Uri UserAccessPolicyUri { get; set; }
    }
}
