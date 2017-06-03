using System.Collections.Generic;

namespace QDocsWeb.Models
{
    public class Question
    {
        public string Id { get; set; }
        public int Votes { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public UserProfile user { get; set; }
        public IEnumerable<Response> Responses { get; set; }
    }
}
