using ESG_App.DBContext;
using ESG_App.Enum;
using ESG_App.ImplService;
using ESG_App.IService;
using ESG_App.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESG_TEST
{
    public class UnitTestSurvey
    {
        [Fact]
        public async void AddSurvey()
        {

            var survey = new Survey()
            {
                SurveyID = 1,
                Name = "Sample Survey",
                Description = "Sample Description",
                Status = SurveyStatus.PUBLISHED,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Year = 2023,
                PublishedAt = DateTime.UtcNow,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedById = 1,
                ModifiedById = 2,
            };

            var mockDbContext = new Mock<ESGDbContext>();
            var dbSetMock = new Mock<DbSet<Survey>>();

            mockDbContext.Setup(x => x.QuestionDBTable).Returns(dbSetMock.Object);

            var surveyService = new SurveyService(mockDbContext.Object);

            // Act
            var result = await surveyService.addSurvey(survey);

            // Assert
            Assert.Equal(result, survey);
            // Verify that Add and SaveChangesAsync were called on the mock


            dbSetMock.Verify(dbSet => dbSet.Add(It.IsAny<survey>()), Times.Once);
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once);


        }
    }
}
