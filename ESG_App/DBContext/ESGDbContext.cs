using ESG_App.Model;
using Microsoft.EntityFrameworkCore;
namespace ESG_App.DBContext
{
    public class ESGDbContext : DbContext
    {

        public ESGDbContext()
        {

        }
        public ESGDbContext(DbContextOptions<ESGDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public virtual DbSet<Question> QuestionDBTable { get; set; }

        public virtual DbSet<Survey> SurveyDBTable { get; set;}

        public DbSet<SurveyQuestions> SurveyQuestionsDBTable { get; set; }



    }
}
