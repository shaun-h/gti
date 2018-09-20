using System.Collections.Generic;
using Newtonsoft.Json;

namespace gti.core.Models.List
{
    public class FeedPackageVersions
    {
        public string Id { get; set; }
        [JsonProperty("versions")]
        public List<string> Versions { get; set; }
    }
}