// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceServer.Contracts;
using ResourceServer.Models;
using System.Threading.Tasks;

namespace ResourceServer.Controllers
{
    [Authorize]
    public class ResourceDescriptionController : Controller
    {
        private readonly IProtectionApiClient _protectionApiClient;

        public ResourceDescriptionController(IProtectionApiClient protectionApiClient)
        {
            _protectionApiClient = protectionApiClient;
        }

        [Route("ResourceDescription/Create")]
        [HttpGet]
        public IActionResult CreateResourceDescription()
        {
            return View(new CreateResourceDescriptionViewModel());
        }

        [Route("ResourceDescription/")]
        [HttpPost]
        public async Task<IActionResult> ResourceDescription([FromBody] CreateResourceDescriptionViewModel resourceDescription)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            ResourceDescriptionModel rd = new ResourceDescriptionModel
            {
                ResourceScopes = resourceDescription.Scopes,
                Description = resourceDescription.Description,
                IconUri = resourceDescription.IconUri,
                Name = resourceDescription.Name,
                Type = resourceDescription.ResourceType
            };

            /*CreateResourceDescriptionResponse response =*/ await _protectionApiClient.CreateResourceDescriptionAsync(rd, accessToken);

            // Cache the response object for the records.

            return new OkResult();
        }
    }
}
