using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QDocsWeb.Models;
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
            return new List<Question>();
        }
        
        [HttpGet("{id}")]
        public Question Get(int id)
        {
            return new Question();
        }
        
        [HttpPost]
        public Question Post([FromBody]Question value)
        {
            return value;
        }
        
        [HttpPost("{id}/votes")]
        public Question PostVote(int id, [FromBody]int value)
        {
            return new Question();
        }

        [HttpPost("{id}/responses")]
        public Response PostResponse(int id, [FromBody]Response value)
        {
            return new Response();
        }

        [HttpPost("{id}/responses/votes")]
        public Response PostResponseVote(int id, [FromBody]int value)
        {
            return new Response();
        }
    }
}
