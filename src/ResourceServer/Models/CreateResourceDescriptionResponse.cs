// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json.Serialization;

namespace ResourceServer.Models
{
    /// <summary>
    /// The response to the Create Resource Description API call.
    /// </summary>
    public class CreateResourceDescriptionResponse
    {
        /// <summary>
        /// The authorization server assigned descrition identifier
        /// </summary>
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        /// <summary>
        /// The URI to set the resource set's access policy.
        /// </summary>
        [JsonPropertyName("user_access_policy_uri")]
        public Uri UserAccessPolicyUri { get; set; }
    }
}
