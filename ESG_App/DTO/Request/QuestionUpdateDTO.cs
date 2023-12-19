namespace ESG_App.DTO.Request
{
    public class QuestionUpdateDTO
    {
        public string QuestionText { get; set; }
        public string Guidance { get; set; }
        public bool Rating { get; set; }
        public int? ParentQuestionId { get; set; }

    }
}
