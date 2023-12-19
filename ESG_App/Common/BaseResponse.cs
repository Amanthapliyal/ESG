namespace ESG_App.Common
{
    public class BaseResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public BaseResponse(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }


        public static BaseResponse<T> Success(T data)
        {
            return new BaseResponse<T>(ResponseHelper.GetCode(ResponseCode.Success),
                ResponseHelper.GetMessage(ResponseCode.Success),
                data);
        }

        public static BaseResponse<T> Fail(ResponseCode responseCode, T? data)
        {
            return new BaseResponse<T>(ResponseHelper.GetCode(responseCode),
                ResponseHelper.GetMessage(responseCode),
                data
            );
        }
    }
}
