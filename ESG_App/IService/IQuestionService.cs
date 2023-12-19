using ESG_App.DTO.Request;
using ESG_App.DTO.Response;
using ESG_App.Model;
using System.Runtime.CompilerServices;

namespace ESG_App.Service
{
    public interface IQuestionService
    {
        public Task<Question> addQuestions(Question question);

        public Task<List<Question>> getAllQuestions();

        public Task<Question> getQuestionsById(int QuestionId);

        public Task<Question> removeQuestionById(int QuestionId);


        public Task<Question> updateQuestion(int QuestionId, QuestionUpdateDTO questionUpdatedto);

    }
}
