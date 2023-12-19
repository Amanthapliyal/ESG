using ESG_App.DBContext;
using ESG_App.ImplService;
using ESG_App.Model;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ESG_TEST
{
    public class UnitTestQuestion
    {
        [Fact]
        public async void AddQuestion()
         {
            var question = new Question()
            {
                QuestionID = 1,
                QuestionText = "sffs",
                Guidance = "sadnja",
                Rating = false,
                ParentQuestionId = 2,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedById = 1,
                ModifiedById = 2,
            };
            

            // Arrange
            var mockDbContext = new Mock<ESGDbContext>();
            var dbSetMock = new Mock<DbSet<Question>>();

          
            mockDbContext.Setup(x => x.QuestionDBTable).Returns(dbSetMock.Object);

            var questionService = new QuestionService(mockDbContext.Object);

            // Act
            var result = await questionService.addQuestions(question);

            // Assert
            Assert.Equal(result, question);
            // Verify that Add and SaveChangesAsync were called on the mock


            dbSetMock.Verify(dbSet => dbSet.Add(It.IsAny<Question>()), Times.Once);
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        

    }

    
    }
}