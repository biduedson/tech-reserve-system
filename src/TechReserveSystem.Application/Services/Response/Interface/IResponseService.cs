using TechReserveSystem.Shared.Communication.Response;

namespace TechReserveSystem.Application.Services.Responses.Interfaces
{
    public interface IResponseService<T>
    {
        Response<T> Success(T data);
        Response<T> Failure(string message, List<string>? errors = null);
    }
}