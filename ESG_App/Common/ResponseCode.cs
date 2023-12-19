namespace ESG_App.Common
{
    public enum ResponseCode
    {
        Success,
        BadRequest,
        Unauthorized,
        Forbidden,
        NotFound,
        InternalServerError,
        InvalidQuestion,
        InvalidSurvey,
        AlreadyPresent,

    }

    public static class ResponseHelper
    {
        private static readonly Dictionary<ResponseCode, (int Code, string Message)> ResponseInfo = new Dictionary<ResponseCode, (int, string)>
            {
                { ResponseCode.Success, (200, "Success") },
                { ResponseCode.BadRequest, (400, "Bad Request") },
                { ResponseCode.Unauthorized, (401, "Unauthorized") },
                { ResponseCode.Forbidden, (403, "Forbidden") },
                { ResponseCode.NotFound, (404, "Not Found") },
                { ResponseCode.InternalServerError, (500, "Internal Server Error") },
                { ResponseCode.InvalidQuestion , (4001, "Invalid Question Id")},
                { ResponseCode.InvalidSurvey, (4002, "Invalid Survey")},
                { ResponseCode.AlreadyPresent , (4003, "Already present you can only update")},
                

            };

        public static int GetCode(ResponseCode code)
        {
            return ResponseInfo.ContainsKey(code) ? ResponseInfo[code].Code : -1;
        }

        public static string GetMessage(ResponseCode code)
        {
            return ResponseInfo.ContainsKey(code) ? ResponseInfo[code].Message : "Unknown";
        }
    }
}
