// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace ResourceServer.Models
{
    public class CreateResourceDescriptionViewModel
    {
        public string[] Scopes { get; set; }

        public string Description { get; set; }

        public string IconUri { get; set; }

        public string Name { get; set; }

        public string ResourceType { get; set; }
    }
}
