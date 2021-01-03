// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using System.Collections.Generic;

namespace ResourceServer.Models
{
    public class ResourceDescriptionsViewModel
    {
        public ResourceDescriptionsViewModel()
        {
            RequestDescriptions = new List<ResourceDescriptionDto>();
        }

        public List<ResourceDescriptionDto> RequestDescriptions { get; set; }
    }
}
