// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using ResourceServer;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
            var result = await client.PostAsync("https://localhost:5001/rs/resource_set", content);

            var returnedContent = result.Content;

            ViewBag.Json = await returnedContent.ReadAsStringAsync();
            return View("json");
        }
    }
}
