namespace TechReserveSystem.Shared.Communication.Response
{
    public class HttpResponse<TData>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public TData? Data { get; set; }
        public IList<string>? Errors { get; set; }

        public static HttpResponse<TData> Ok(TData data, string message) =>
        new HttpResponse<TData> { Success = true, Message = message, Data = data };

        public static HttpResponse<TData> Error(string message, IList<string> errors) =>
        new HttpResponse<TData> { Success = false, Message = message, Errors = errors };

        public static HttpResponse<TData> AuthError(string message) =>
        new HttpResponse<TData> { Success = false, Message = message };

    }
}