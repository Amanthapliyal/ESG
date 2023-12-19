using ESG_App.Model;

namespace ESG_App.DTO.Response
{
    public class SurveyQuestionsResponse
    {
        public Survey? survey {  get; set; }

        public List<Question> questions { get; set; }
    }
}
