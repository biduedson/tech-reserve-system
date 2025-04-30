namespace TechReserveSystem.Shared.Communication.Request
{
    public class HttpRequest<TData> where TData : class
    {
        public TData Data { get; set; }
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

    }
}