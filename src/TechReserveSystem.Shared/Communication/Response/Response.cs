namespace TechReserveSystem.Shared.Communication.Response
{
    public class Response<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }

        private Response(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static Response<T> SuccessResponse(T data, string message)
        {
            return new Response<T>(true, message, data);
        }

        public static Response<T> FailureResponse(string message)
        {
            return new Response<T>(false, message, default);
        }
    }

}

