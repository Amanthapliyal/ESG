using ESG_App.DBContext;
using ESG_App.DTO.Response;
using ESG_App.Exceptions;
using ESG_App.Model;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using ESG_App.Common;
using System.Net;
using Serilog;

namespace ESG_App.IService
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly ESGDbContext _dbContext;

        public SurveyQuestionService(ESGDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SurveyQuestionsPostResponse> addSurveyQuestion(int surveyId, List<int> questionList)
        {
            var surveyObj = await _dbContext.SurveyQuestionsDBTable.Where(i => i.SurveyId == surveyId).FirstOrDefaultAsync();
            if (surveyObj != null)
            {
                throw new CommonException(ResponseCode.AlreadyPresent, HttpStatusCode.BadRequest);
            }


            foreach (int i in questionList)
            {
                SurveyQuestions surveyQuestions = new SurveyQuestions()
                {
                    SurveyId = surveyId,
                    QuestionId = i,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                };

                _dbContext.SurveyQuestionsDBTable.Add(surveyQuestions);
                _dbContext.SaveChanges();

            }

            SurveyQuestionsPostResponse response = new SurveyQuestionsPostResponse();
            response.survey = surveyId;

            response.questions = questionList;

            return response;


        }

        public async Task<List<SurveyQuestionsResponse>> getAllSurveyQuestions()
        {
            List<SurveyQuestionsResponse> response = new List<SurveyQuestionsResponse>();

            var groupedQuestionIds = await _dbContext.SurveyQuestionsDBTable
                                                     .GroupBy(x => x.SurveyId)
                                                     .Select(o => new
                                                     {
                                                         SurveyId = o.Key,
                                                         QuestionIds = o.Select(x => x.QuestionId).ToList()
                                                     })
                                                     .ToListAsync();

            foreach (var i in groupedQuestionIds)
            {
                response.Add(await this.convertToSurveyQuestionsResponse(i.SurveyId, i.QuestionIds));
            }

            return response;

        }

        public async Task<SurveyQuestionsPostResponse> removeSurveyQuestionBySurveyId(int surveyId)
        {
            var surveyQuestionList = await _dbContext.SurveyQuestionsDBTable
                                                     .Where(i => i.SurveyId == surveyId)
                                                     .ToListAsync();
            if (surveyQuestionList.Count == 0)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
            }

            List<int> questions = surveyQuestionList.Select(i => i.QuestionId).ToList();


            _dbContext.SurveyQuestionsDBTable.RemoveRange(surveyQuestionList);

            _dbContext.SaveChanges();

            SurveyQuestionsPostResponse response = new SurveyQuestionsPostResponse();
            response.survey = surveyId;

            response.questions = questions;


            return response;
        }

        public async Task<SurveyQuestionsPostResponse> updateSurveyQuestion(int surveyId, List<int> questionList)
        {
            var surveyQuestionList = await _dbContext.SurveyQuestionsDBTable.Where(i => i.SurveyId == surveyId).ToListAsync();

            if (surveyQuestionList == null)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
            }

            _dbContext.SurveyQuestionsDBTable.RemoveRange(surveyQuestionList);

            _dbContext.SaveChanges();

            return await this.addSurveyQuestion(surveyId, questionList);


        }

        public async Task<SurveyQuestionsResponse> convertToSurveyQuestionsResponse(int surveyId, List<int> questionId)
        {
            var surveyObj = await _dbContext.SurveyDBTable.FindAsync(surveyId);

            if (surveyObj == null)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
            }

            List<Question> questionList = new List<Question>();

            foreach (var i in questionId)
            {
                var question = await _dbContext.QuestionDBTable.FindAsync(i);

                if (question == null)
                {
                    throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
                }
                else
                {
                    questionList.Add(question);
                }
            }

            SurveyQuestionsResponse surveyQuestionsResponse = new SurveyQuestionsResponse()
            {
                survey = surveyObj,
                questions = questionList
            };

            return surveyQuestionsResponse;

        }


    }
}
