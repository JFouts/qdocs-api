using System.Collections.Generic;

namespace QDocsWeb.Services.Models
{
    public class Response
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}
