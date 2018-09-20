using System.Collections.Generic;
using Newtonsoft.Json;

namespace gti.core.Models.List
{
    public class FeedIndex
    {
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("resources")]
        public List<FeedIndexResource> Resources { get; set; }
    }
}