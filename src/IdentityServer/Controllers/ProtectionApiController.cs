// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using IdentityServer.UmaAs.Contracts;
using IdentityServer.UmaAs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using Uma.IdentityServer4;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Authorize(ProtectionApi.PolicyName)]
    public class ProtectionApiController : ControllerBase
    {
        private readonly IResourceDescriptionStore _store;
        public ProtectionApiController(IResourceDescriptionStore store)
        {
            _store = store;
        }

        [Route("rs/resource_set")]
        public IActionResult Get()
        {
            IDictionary<Guid, ResourceDescription> rds = _store.GetResourceDescriptions(User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value);
            if (rds != null)
            {
                return new JsonResult(rds.Keys);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [Route("rs/resource_set/{id}")]
        public IActionResult Get(Guid id)
        {
            ResourceDescription rd = _store.GetResourceDescription(User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value, id);
            if (rd != null)
            {
                return new JsonResult(rd.ToDto(id));
            }
            else return new NotFoundResult();
        }

        [HttpPost]
        [Route("rs/resource_set")]
        public IActionResult Post([FromBody] ResourceDescription resourceDescriptionJson)
        {
            string domainName = HttpContext.Request.Host.ToUriComponent();
            try
            {
                Guid id = Guid.NewGuid();
                ResourceSetCreationResponse rs = new ResourceSetCreationResponse { Id = id, UserAccessPolicyUri = new Uri($"https://{domainName}/connect/permissionregister/{id}") };
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

        [HttpPatch]
        [Route("rs/resource_set/{id}")]
        public IActionResult Patch(Guid id, [FromBody] ResourceDescription resourceDescriptionJson)
        {
            string userId = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;

            bool deleted = _store.DeleteDescription(userId, id);

            if (deleted)
            {
                _store.AddDescription(userId, id, resourceDescriptionJson);
                return new JsonResult(new { _id = id });
            }
            else return new NotFoundResult();
        }

        [HttpDelete]
        [Route("rs/resource_set/{id}")]
        public IActionResult Delete(Guid id)
        {
            string userId = User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value;

            bool deleted = _store.DeleteDescription(userId, id);

            if (deleted)
            {
                return new NoContentResult();
            }
            else return new NotFoundResult();
        }
    }
}
