namespace TechReserveSystem.Shared.Communication.Response
{
    public class Response<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }
        public List<string>? Errors { get; private set; }

        public Response(bool success, string message, T? data, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }
    }
}
