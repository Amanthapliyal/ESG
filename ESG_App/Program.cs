using ESG_App.DBContext;
using ESG_App.Exceptions;
using ESG_App.ImplService;
using ESG_App.IService;
using ESG_App.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);// 

//The CreateBuilder method is a static method of the WebApplication class.
//public static WebApplicationBuilder CreateBuilder();
//It is used to create a new instance of the WebApplicationBuilder class, which
//is a builder for configuring and building a web application.

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddDbContext<ESGDbContext>(
                    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db1"))
                    );


//it adds a DbContext to the dependency injection container. It configures the Entity Framework Core
//to use PostgreSQL as the database, and the connection string is retrieved from the configuration settings.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyQuestionService, SurveyQuestionService>(); 



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("log/MyBeautifullogs-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
