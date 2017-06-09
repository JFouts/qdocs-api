using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QDocsWeb.Controllers.Models;
using QDocsWeb.Controllers.Services;

namespace QDocsWeb.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _questionService.GetAll();
        }
        
        [HttpGet("{id}")]
        public Question Get(string id)
        {
            return _questionService.Get(id);
        }
        
        [HttpPost]
        public Question Post([FromBody]Question value)
        {
            return _questionService.Create(value);
        }
        
        [HttpPost("{id}/votes")]
        public Question PostVote(string id, [FromBody]int value)
        {
            return _questionService.SetVote(id, "0", value);
        }

        [HttpPost("{id}/responses")]
        public Question PostResponse(string id, [FromBody]Response value)
        {
            return _questionService.AddResponse(id, value);
        }

        [HttpPost("{id}/responses/{responseId}/votes")]
        public Response PostResponseVote(string id, string responseId, [FromBody]int value)
        {
            return _questionService.UpdateResponseVote(id, responseId, "0", value);
        }
    }
}
