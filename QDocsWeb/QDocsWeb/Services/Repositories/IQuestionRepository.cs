using QDocsWeb.Services.Models;
using System.Collections.Generic;

namespace QDocsWeb.Services.Repositories
{
    public interface IQuestionRepository
    {
        Question GetById(string id);
        IEnumerable<Question> GetAll();
        Question Upsert(Question value);
    }
}
