using Newtonsoft.Json;
using System.Collections.Generic;

namespace QDocsWeb.Controllers.Models
{
    public class Question
    {
        public string Id { get; set; }
        public int Votes { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
