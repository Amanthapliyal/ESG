using ESG_App.DBContext;
using ESG_App.DTO.Request;
using ESG_App.DTO.Response;
using ESG_App.Exceptions;
using ESG_App.Model;
using ESG_App.Service;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ESG_App.Common;
using System.Net;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace ESG_App.ImplService
{
    public class QuestionService : IQuestionService
    {
        private readonly ESGDbContext _dbContext;

        
        public QuestionService(ESGDbContext dbContext)
        {
            //_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;

        }

        public async Task<Question> addQuestions(Question question)
        {
            if (question.IsDeleted == null)
            {
                question.IsDeleted = false;
            }
            _dbContext.QuestionDBTable.Add(question);
            _dbContext.SaveChanges();
            return question;
        }

        public async Task<List<Question>> getAllQuestions()
        {
            

            using (_dbContext)
            {
                var questionList = await _dbContext.QuestionDBTable.ToListAsync();

                if (questionList.Count == 0)
                {
                    Log.Error("No Element fount in Question Table");
                    throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
                }

                return questionList;

            }

        }

        public async Task<Question> getQuestionsById(int QuestionId)
        {
            var questionObj = await _dbContext.QuestionDBTable.FindAsync(QuestionId);


            if (questionObj == null)
            {
                Log.Error("No Element found with QuestionId: {0}", QuestionId);
                throw new CommonException(ResponseCode.InvalidQuestion, HttpStatusCode.NotFound);
            }

            return questionObj;

        }

        public async Task<Question> removeQuestionById(int QuestionId)
        {
            var questionObj = await _dbContext.QuestionDBTable.FindAsync(QuestionId);

            if (questionObj == null)
            {
                throw new CommonException(ResponseCode.InvalidQuestion, HttpStatusCode.NotFound);
            }

            if (questionObj.IsDeleted == true)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.BadRequest);
            }

            questionObj.IsDeleted = true;


            var surveyQuestionList = await _dbContext.SurveyQuestionsDBTable.Where(i => i.QuestionId == QuestionId).ToListAsync();

            if (surveyQuestionList != null)
                _dbContext.SurveyQuestionsDBTable.RemoveRange(surveyQuestionList);



            _dbContext.SaveChanges();



            return questionObj;
        }

        public async Task<Question> updateQuestion(int QuestionId, QuestionUpdateDTO questionUpdatedto)
        {
            var questionObj = await _dbContext.QuestionDBTable.FindAsync(QuestionId);


            if (questionObj == null)
            {
                throw new CommonException(ResponseCode.InvalidQuestion, HttpStatusCode.NotFound);
            }

            if (questionObj.IsDeleted == true)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.BadRequest);
            }

            questionObj.QuestionText = questionUpdatedto.QuestionText;
            questionObj.Guidance = questionUpdatedto.Guidance;
            questionObj.Rating = questionUpdatedto.Rating;
            questionObj.ParentQuestionId = questionUpdatedto.ParentQuestionId;
            questionObj.ModifiedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return questionObj;


        }
    }
}
