namespace QDocsWeb.Models
{
    public class Response
    {
        public string Id { get; set; }
        public int Votes { get; set; }
        public string Content { get; set; }
        public UserProfile User { get; set; }
    }
}