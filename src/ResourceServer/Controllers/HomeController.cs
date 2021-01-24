// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResourceServer.Contracts;
using ResourceServer.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProtectionApiClient _protectionApiClient;

        public HomeController(ILogger<HomeController> logger, IProtectionApiClient protectionApiClient)
        {
            _logger = logger;
            _protectionApiClient = protectionApiClient;
        }

        public async Task<IActionResult> Index()
        {
            ResourceDescriptionsViewModel requestDescriptions = new ResourceDescriptionsViewModel();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            IList<ResourceDescriptionDto> resourceDescriptions = await _protectionApiClient.GetResourceDescriptionsAsync(accessToken);

            requestDescriptions.ResourceDescriptions = resourceDescriptions;

            return View(requestDescriptions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
