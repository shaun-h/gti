using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using gti.core.Interfaces;
using gti.core.Models;
using gti.core.Models.List;
using Newtonsoft.Json;

namespace gti.core.Operations
{
    public class ListNugetOperation : IOperation
    {
        private ListOperationOptions _options;
        
      
        public void SetOperationOptions(IOperationOptions options)
        {
            var op = options as ListOperationOptions;
            if (op == null)
            {
                throw new ArgumentException(nameof(options));
            }
            else
            {
                _options = op;
            }
        }

        public void PerformOperation()
        {
            Console.WriteLine($"Checking feed: {_options.FeedUrl}");
            var feed = GetResponseFromJson<FeedIndex>(_options.FeedUrl).Result;
            var catalogUri = feed.Resources.SingleOrDefault(x => x.Type == "Catalog/3.0.0")?.Id;
            var registrationUri = feed.Resources.SingleOrDefault(x => x.Type == "RegistrationsBaseUrl")?.Id;
            var packagebaseUri = feed.Resources.SingleOrDefault(x => x.Type == "PackageBaseAddress/3.0.0")?.Id;
            if (catalogUri != null)
            {
                var catalog = GetResponseFromJson<FeedCatalog>(catalogUri).Result;
                foreach (var catalogItem in catalog.Items)
                {
                    var ciUri = catalogItem.Id;
                    Console.WriteLine($"Getting data from {ciUri}");
                    var pageItems = GetResponseFromJson<FeedCatalogPage>(ciUri).Result.Items;
                    foreach (var feedCatalogPageItem in pageItems)
                    {
                        var versionsUri = packagebaseUri + feedCatalogPageItem.NuGetId.ToLowerInvariant() +
                                          "/index.json";
                        var packageVersions = GetResponseFromJson<FeedPackageVersions>(versionsUri).Result;
                        packageVersions.Id = feedCatalogPageItem.NuGetId;
                        foreach (var version in packageVersions.Versions)
                        {
                            var nuspecUri = packagebaseUri + feedCatalogPageItem.NuGetId.ToLowerInvariant() +"/"+ version.ToLowerInvariant() +
                                              "/" +feedCatalogPageItem.NuGetId.ToLowerInvariant() + ".nuspec";
                            var data = GetStream(nuspecUri).Result;
                            SaveFile(data, $"nuspecs/{feedCatalogPageItem.NuGetId.ToLowerInvariant()}.{version.ToLowerInvariant()}.nuspec").ConfigureAwait(false);
                        }
                    }
                }
            }
        }

        private async Task<T> GetResponseFromJson<T>(string uri)
        {
            var client = new HttpClient();
//            client.BaseAddress = new Uri(uri);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                throw new HttpRequestException($"Request to {response.RequestMessage.RequestUri} failed, with status code {response.StatusCode}");
            }
        }

        private async Task<Stream> GetStream(string uri)
        {
            var client = new HttpClient();
//            client.BaseAddress = new Uri(uri);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStreamAsync();
                return data;
            }
            else
            {
                throw new HttpRequestException($"Request to {response.RequestMessage.RequestUri} failed, with status code {response.StatusCode}");
            }
        }

        private async Task SaveFile(Stream contents, string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await contents.CopyToAsync(stream);
            } 
        }
    }
}