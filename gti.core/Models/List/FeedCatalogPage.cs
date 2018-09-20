using Newtonsoft.Json;
using System.Collections.Generic;


namespace gti.core.Models.List
{
    public class FeedCatalogPage
    {
        [JsonProperty("@id")] 
        public string Id { get; set; }
        [JsonProperty("items")]
        public List<FeedCatalogPageItem> Items { get; set; }
    }
}