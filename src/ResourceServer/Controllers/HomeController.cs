// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using ResourceServer;
using ResourceServer.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static readonly Uri authServerBaseUri = new Uri("https://localhost:5001/rs/resource_set/");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ResourceDescriptionsViewModel requestDescriptions = new ResourceDescriptionsViewModel();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var result = await client.GetAsync(authServerBaseUri);

            if (result.IsSuccessStatusCode)
            {
                string idStringsJson = await result.Content.ReadAsStringAsync();
                string[] resourceDescriptionIds = JsonSerializer.Deserialize<string[]>(idStringsJson);

                foreach (string id in resourceDescriptionIds)
                {
                    Uri api = new Uri(authServerBaseUri, id);
                    // Get the resource description
                    result = await client.GetAsync(api);
                    if (result.IsSuccessStatusCode)
                    {
                        var rdJson = await result.Content.ReadAsStringAsync();
                        ResourceDescriptionDto rdDto = JsonSerializer.Deserialize<ResourceDescriptionDto>(rdJson);

                        requestDescriptions.RequestDescriptions.Add(rdDto);
                    }
                }
            }

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

        public IActionResult CreateResourceDescription()
        {
            return View(new CreateResourceDescriptionViewModel());
        }

        public async Task<IActionResult> CallProtectionApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            ResourceDescription rd = new ResourceDescription
            {
                ResourceScopes = new string[] { "read", "write" },
                Description = "Description of test resource",
                IconUri = "http://icon.uri",
                Name = "TestResourceName",
                Type = "TestResourceType"
            };
            StringContent content = new StringContent(JsonSerializer.Serialize(rd), Encoding.UTF8, MediaTypeNames.Application.Json);
            var result = await client.PostAsync(authServerBaseUri, content);

            var returnedContent = result.Content;

            ViewBag.Json = await returnedContent.ReadAsStringAsync();
            return View("json");
        }
    }
}
