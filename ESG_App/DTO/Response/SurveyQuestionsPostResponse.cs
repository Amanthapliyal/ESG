using ESG_App.Model;

namespace ESG_App.DTO.Response
{
    public class SurveyQuestionsPostResponse 
    {
        public int survey { get; set; }
        public List<int> questions { get; set; }
    }
}
