using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uma.IdentityServer4.ResponseHandling
{
    public interface IUmaConfigurationResponseGenerator
    {
        Task<Dictionary<string, object>> CreateConfigurationDocumentAsync(string baseUrl, string issuerUri);
    }
}
