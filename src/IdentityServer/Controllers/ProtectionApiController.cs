// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using IdentityServer.UmaAs.Contracts;
using IdentityServer.UmaAs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using Uma.IdentityServer4;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Authorize(ProtectionApi.PolicyName)]
    [Route("rs/resource_set")]
    public class ProtectionApiController : ControllerBase
    {
        private IResourceDescriptionStore _store;
        public ProtectionApiController(IResourceDescriptionStore store)
        {
            _store = store;
        }

        [Route("rs/resource_set/{id}")]
        public IActionResult Get(string id)
        {
            ResourceDescription rd = _store.GetResourceDescription(User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value, new Guid(id));
            return new JsonResult(JsonSerializer.Serialize(rd));
        }

        public IActionResult Post([FromBody] ResourceDescription resourceDescriptionJson)
        {
            string domainName = HttpContext.Request.Host.ToUriComponent();
            try
            {
                Guid id = Guid.NewGuid();
                ResourceSetCreationResponse rs = new ResourceSetCreationResponse { Id = id, UserAccessPolicyUri = new Uri($"https://{domainName}/connect/permissionregister/{id.ToString()}") };
                _store.AddDescription(User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value, rs.Id, resourceDescriptionJson);

                CreatedResult result = new CreatedResult(new Uri($"https://{domainName}/rs/resource_set/{rs.Id}"), rs);
                result.ContentTypes.Add(MediaTypeNames.Application.Json);
                return result;
            }
            catch (ArgumentNullException)
            {
                return BadRequest("A resource description must be present.");
            }
            catch (JsonException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotSupportedException ns)
            {
                return BadRequest($"Not supported: {ns.Message}");
            }
        }
    }
}
