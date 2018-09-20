using Newtonsoft.Json;

namespace gti.core.Models.List
{
    public class FeedCatalogItem
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
    }
}