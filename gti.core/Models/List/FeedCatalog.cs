using System.Collections.Generic;
using Newtonsoft.Json;

namespace gti.core.Models.List
{
    public class FeedCatalog
    {
        [JsonProperty("@id")] 
        public string Id { get; set; }
        [JsonProperty("items")]
        public List<FeedCatalogItem> Items { get; set; }
    }
}