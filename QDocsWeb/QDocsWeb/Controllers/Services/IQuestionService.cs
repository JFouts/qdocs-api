using System.Collections.Generic;
using QDocsWeb.Controllers.Models;

namespace QDocsWeb.Controllers.Services
{
    public interface IQuestionService
    {
        IEnumerable<Question> GetAll();
        Question Get(string id);
        Question Create(Question value);
        Question SetVote(string id, string userId, int value);
        Question AddResponse(string id, Response value);
        Response UpdateResponseVote(string id, string responseId, string userId, int value);
    }
}
