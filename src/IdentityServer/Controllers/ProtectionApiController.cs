using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uma.IdentityServer4;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Authorize(ProtectionApi.PolicyName)]
    [Route("rs/resource_set")]
    public class ProtectionApiController : ControllerBase
    {
        public IActionResult Get()
        {
            var claims = from c in User.Claims select new { c.Type, c.Value };
            return new JsonResult(claims);
        }
    }
}
