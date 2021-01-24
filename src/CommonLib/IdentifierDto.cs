// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace CommonLib
{
    public class IdentifierDto
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
    }
}
