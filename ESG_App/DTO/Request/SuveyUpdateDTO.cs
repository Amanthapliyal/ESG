using ESG_App.Enum;

namespace ESG_App.DTO.Request
{
    public class SuveyUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SurveyStatus Status { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Year { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}
