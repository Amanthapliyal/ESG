
using ESG_App.Common;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace ESG_App.Exceptions
{
    public class GlobalExceptionHandler 
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {  
            context.Response.ContentType = "application/json";
            BaseResponse<object> response;
            if (exception is CommonException businessException)
            {
                context.Response.StatusCode = (int)businessException.HttpStatusCode;
                response = BaseResponse<object>.Fail(businessException.Code, default);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = BaseResponse<object>.Fail(ResponseCode.InternalServerError, default);
            }

            var jsonResponse = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
