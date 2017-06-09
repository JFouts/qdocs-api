using System.Collections.Generic;
using QDocsWeb.Controllers.Models;
using QDocsWeb.Controllers.Services;
using QDocsWeb.Services.Repositories;
using System.Linq;
using System;

namespace QDocsWeb.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        
        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Question Create(Question value)
        {
            var question = Convert(value);
            
            question.Id = null;
            question.Responses = new List<Models.Response>();
            question.Votes = new List<Models.Vote>
            {
                new Models.Vote
                {
                    UserId = question.UserId,
                    VoteWeight = 1
                }
            };

            var response = _questionRepository.Upsert(question);

            return Convert(response);
        }

        public Question Get(string id)
        {
            var response = _questionRepository.GetById(id);
            
            return Convert(response);
        }

        public IEnumerable<Question> GetAll()
        {
            var response = _questionRepository.GetAll();

            return response.Select(x => Convert(x));
        }

        public Question SetVote(string id, string userId, int value)
        {
            //TODO: This is terrible... wont scale becuase it so non-atomic
            var question = _questionRepository.GetById(id);
            var votes = question.Votes.ToList();
            var vote = votes.FirstOrDefault(x => x.UserId == userId);
            if(vote == null)
            {
                votes.Add(new Models.Vote
                {
                    UserId = userId,
                    VoteWeight = value
                });
            }
            else
            {
                vote.VoteWeight = value;
            }

            question.Votes = votes;
            var newQuestion = _questionRepository.Upsert(question);
            return Convert(newQuestion);
        }

        public Question AddResponse(string id, Response value)
        {
            //TODO: This is terrible... wont scale becuase it so non-atomic
            var question = _questionRepository.GetById(id);
            var response = Convert(value);

            response.Id = Guid.NewGuid().ToString();
            response.Votes = new List<Models.Vote>
            {
                new Models.Vote
                {
                    UserId = question.UserId,
                    VoteWeight = 1
                }
            };

            var responses = question.Responses.ToList();
            responses.Add(response);
            question.Responses = responses;

            var newQuestion = _questionRepository.Upsert(question);
            return Convert(newQuestion);
        }

        public Response UpdateResponseVote(string id, string responseId, string userId, int value)
        {
            //TODO: This is terrible... wont scale becuase it so non-atomic
            var question = _questionRepository.GetById(id);

            var response = question.Responses.FirstOrDefault(x => x.Id == responseId);

            if(response == null)
            {
                return null;
            }

            var votes = response.Votes.ToList();
            var vote = votes.FirstOrDefault(x => x.UserId == userId);
            if (vote == null)
            {
                votes.Add(new Models.Vote
                {
                    UserId = userId,
                    VoteWeight = value
                });
            }
            else
            {
                vote.VoteWeight = value;
            }
            response.Votes = votes;

            var newQuestion = _questionRepository.Upsert(question);
            return Convert(newQuestion.Responses.FirstOrDefault(x => x.Id == response.Id));
        }

        private Question Convert(Models.Question question)
        {
            return new Question
            {
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                UserId = question.UserId,
                Responses = question.Responses?.Select(x => Convert(x)),
                Votes = question.Votes?.Sum(x => x.VoteWeight) ?? 0
            };
        }

        private Models.Question Convert(Question question)
        {
            return new Models.Question
            {
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                UserId = question.UserId,
                Responses = question.Responses?.Select(x => Convert(x)),
                Votes = null
            };
        }

        private Response Convert(Models.Response response)
        {
            return new Response
            {
                Id = response.Id,
                Content = response.Content,
                UserId = response.UserId,
                Votes = response.Votes?.Sum(x => x.VoteWeight) ?? 0
            };
        }

        private Models.Response Convert(Response response)
        {
            return new Models.Response
            {
                Id = response.Id,
                Content = response.Content,
                UserId = response.UserId,
                Votes = null
            };
        }
    }
}
