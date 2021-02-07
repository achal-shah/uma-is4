// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using IdentityServer.UmaAs;
using IdentityServer.UmaAs.Contracts;
using IdentityServer.UmaAs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
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
            //IDictionary<Guid, ResourceDescription> rds = _store.GetResourceDescriptions(User.Claims.Where(c => c.Type == "sub").FirstOrDefault().Value);
            IDictionary<Guid, ResourceDescription> rds = _store.GetResourceDescriptions(User.FindFirstValue(ClaimTypes.NameIdentifier));
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
            ResourceDescription rd = _store.GetResourceDescription(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
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
                _store.AddDescription(User.FindFirstValue(ClaimTypes.NameIdentifier), rs.Id, resourceDescriptionJson);

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

        [HttpPut]
        [Route("rs/resource_set/{id}")]
        public IActionResult Put(Guid id, [FromBody] ResourceDescription resourceDescriptionJson)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool deleted = _store.DeleteDescription(userId, id);

            if (deleted)
            {
                _store.AddDescription(userId, id, resourceDescriptionJson);
                return new JsonResult(new IdentifierDto { Id = id.ToString() });
            }
            else return new NotFoundResult();
        }

        [HttpDelete]
        [Route("rs/resource_set/{id}")]
        public IActionResult Delete(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool deleted = _store.DeleteDescription(userId, id);

            if (deleted)
            {
                return new NoContentResult();
            }
            else return new NotFoundResult();
        }
    }
}
