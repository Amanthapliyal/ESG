using ESG_App.Common;
using ESG_App.DBContext;
using ESG_App.DTO.Request;
using ESG_App.Exceptions;
using ESG_App.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace ESG_App.IService
{
    public class SurveyService : ISurveyService
    {
        private readonly ESGDbContext _dbContext;

        public SurveyService(ESGDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Survey> addSurvey(Survey survey)
        {
            _dbContext.SurveyDBTable.Add(survey);
            _dbContext.SaveChanges();
            return survey;
        }

        public async Task<List<Survey>> getAllSurvey()
        {
            var SurveyList = await _dbContext.SurveyDBTable.ToListAsync();
            if (SurveyList == null)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
            }
            return SurveyList;
        }

        public async Task<Survey> getSurveyById(int surveyId)
        {
            var SurveyObj = await _dbContext.SurveyDBTable.FindAsync(surveyId);

            if (SurveyObj == null)
            {
                throw new CommonException(ResponseCode.InvalidSurvey, HttpStatusCode.NotFound);
            }

            return SurveyObj;

        }

        public async Task<Survey> removeSurveyById(int surveyId)
        {
            var SurveyObj = await _dbContext.SurveyDBTable.FindAsync(surveyId);

            if (SurveyObj == null)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
            }

            _dbContext.SurveyDBTable.Remove(SurveyObj);

            var surveyQuestionList = await _dbContext.SurveyQuestionsDBTable.Where(i => i.SurveyId == surveyId).ToListAsync();

            if (surveyQuestionList != null)
                _dbContext.SurveyQuestionsDBTable.RemoveRange(surveyQuestionList);

            _dbContext.SaveChanges();

            return SurveyObj;
        }

        public async Task<Survey> updateSurveyById(int surveyId, SuveyUpdateDTO suveyUpdateDTO)
        {
            var SurveyObj = await _dbContext.SurveyDBTable.FindAsync(surveyId);

            if (SurveyObj == null)
            {
                throw new CommonException(ResponseCode.NotFound, HttpStatusCode.NotFound);
            }
            SurveyObj.Name = suveyUpdateDTO.Name;
            SurveyObj.Description = suveyUpdateDTO.Description;
            SurveyObj.Status = suveyUpdateDTO.Status;
            SurveyObj.StartDate = suveyUpdateDTO.StartDate;
            SurveyObj.EndDate = suveyUpdateDTO.EndDate;
            SurveyObj.Year = suveyUpdateDTO.Year;
            SurveyObj.PublishedAt = suveyUpdateDTO.PublishedAt;


            _dbContext.SaveChanges();

            return SurveyObj;
        }
    }
}
