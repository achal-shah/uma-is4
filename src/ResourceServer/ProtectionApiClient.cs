// Copyright (c) Achal Shah. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using CommonLib;
using ResourceServer.Contracts;
using ResourceServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResourceServer
{
    /// <summary>
    /// Implementation of IProtectionApiClient.
    /// </summary>
    public class ProtectionApiClient : IProtectionApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _remoteServiceBaseUri = new Uri("https://localhost:5001/rs/resource_set/");

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="client">The http client using DI.</param>
        public ProtectionApiClient(HttpClient client)
        {
            _httpClient = client;
        }

        /// <inheritdoc/>
        public async Task<CreateResourceDescriptionResponse> CreateResourceDescriptionAsync(ResourceDescriptionModel resourceDescription, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            StringContent content = new StringContent(JsonSerializer.Serialize(resourceDescription), Encoding.UTF8, MediaTypeNames.Application.Json);
            var result = await _httpClient.PostAsync(_remoteServiceBaseUri, content);

            var returnedContent = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CreateResourceDescriptionResponse>(returnedContent);
        }

        /// <inheritdoc/>
        public async Task<IList<ResourceDescriptionDto>> GetResourceDescriptionsAsync(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var result = await _httpClient.GetAsync(_remoteServiceBaseUri);

            List<ResourceDescriptionDto> resourceDescriptions = new List<ResourceDescriptionDto>();
            if (result.IsSuccessStatusCode)
            {
                string idStringsJson = await result.Content.ReadAsStringAsync();
                string[] resourceDescriptionIds = JsonSerializer.Deserialize<string[]>(idStringsJson);

                foreach (string id in resourceDescriptionIds)
                {
                    Uri api = new Uri(_remoteServiceBaseUri, id);
                    // Get the resource description
                    result = await _httpClient.GetAsync(api);
                    if (result.IsSuccessStatusCode)
                    {
                        var rdJson = await result.Content.ReadAsStringAsync();
                        ResourceDescriptionDto rdDto = JsonSerializer.Deserialize<ResourceDescriptionDto>(rdJson);

                        resourceDescriptions.Add(rdDto);
                    }
                }
            }
            return resourceDescriptions;
        }
    }
}
