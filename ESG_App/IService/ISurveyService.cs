using ESG_App.DTO.Request;
using ESG_App.Model;

namespace ESG_App.IService
{
    public interface ISurveyService
    {
        public Task<Survey> addSurvey(Survey survey);

        public Task<List<Survey>> getAllSurvey();

        public Task<Survey> getSurveyById(int surveyId);

        public Task<Survey> removeSurveyById(int surveyId);

        public Task<Survey> updateSurveyById(int surveyId, SuveyUpdateDTO suveyUpdateDTO);  
    }
}
