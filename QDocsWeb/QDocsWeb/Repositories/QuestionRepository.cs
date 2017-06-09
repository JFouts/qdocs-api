using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using QDocsWeb.Services.Models;
using QDocsWeb.Services.Repositories;
using System.Collections.Generic;

namespace QDocsWeb.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DocumentClient _documentClient;

        private const string DatabaseId = "question-db";
        private const string CollectionId = "questions";

        public QuestionRepository(DocumentClient documentClient)
        {
            _documentClient = documentClient;
        }

        public Question GetById(string id)
        {
            return _documentClient.ReadDocumentAsync<Question>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id)).Result;
        }

        public IEnumerable<Question> GetAll()
        {
            return _documentClient.CreateDocumentQuery<Question>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
        }

        public Question Upsert(Question value)
        {
            Document response = _documentClient.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), value).Result;
            return _documentClient.ReadDocumentAsync<Question>(response.SelfLink).Result;
        }
    }
}
