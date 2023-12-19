namespace ESG_App.DTO.Response
{
    public class QuestionResponse
    {
        public string QuestionText { get; set; }
        public string Guidance { get; set; }
        public bool Rating { get; set; }
        public int? ParentQuestionId { get; set;}
    }
}
