using Newtonsoft.Json;

namespace gti.core.Models.List
{
    public class FeedIndexResource
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}