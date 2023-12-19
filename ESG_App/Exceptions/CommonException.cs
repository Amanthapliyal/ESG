using ESG_App.Common;
using System.Net;

namespace ESG_App.Exceptions
{
    public class CommonException : Exception
    {
        public ResponseCode Code { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public CommonException(ResponseCode code, HttpStatusCode httpStatusCode)
        {
            Code = code;
            HttpStatusCode = httpStatusCode;
        }

        public override string ToString()
        {
            return "{" + ResponseHelper.GetCode(Code) + " : " + ResponseHelper.GetMessage(Code) + "}" + "\n" +
                   this.StackTrace;
        }

    }
}
