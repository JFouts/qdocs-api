using Newtonsoft.Json;
using System.Collections.Generic;

namespace QDocsWeb.Services.Models
{
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
