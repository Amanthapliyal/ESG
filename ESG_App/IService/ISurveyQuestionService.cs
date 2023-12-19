using ESG_App.DTO.Response;

namespace ESG_App.IService
{
    public interface ISurveyQuestionService
    {
        public Task<SurveyQuestionsPostResponse> addSurveyQuestion(int surveyId, List<int> questionList);

        public Task<SurveyQuestionsPostResponse> removeSurveyQuestionBySurveyId(int surveyId);

        public Task<List<SurveyQuestionsResponse>> getAllSurveyQuestions();

        public Task<SurveyQuestionsPostResponse> updateSurveyQuestion(int surveyId, List<int> questionList);
    }
}
