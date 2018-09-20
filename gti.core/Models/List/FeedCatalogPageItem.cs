using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace gti.core.Models.List
{
    public class FeedCatalogPageItem
    {
        [JsonProperty("nuget:id")]
        public string NuGetId { get; set; }
    }
}