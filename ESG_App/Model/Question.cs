using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESG_App.Model
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Guidance { get; set; }
        public bool Rating { get; set; } 
        public int? ParentQuestionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 
        public int CreatedById { get; set; } 
        public int ModifiedById { get; set; }

        //var expectedquestion = new Question()
        //    {
        //         QuestionID =1,
        //         QuestionText ="sffs",
        //         Guidance ="sadnja",
        //         Rating = false,
        //         ParentQuestionId = 2, 
        //         IsDeleted = false,
        //         CreatedAt = DateTime.UtcNow,
        //         ModifiedAt = DateTime.UtcNow,
        //         CreatedById = 1,
        //         ModifiedById = 2,
        //     };
    }
}
