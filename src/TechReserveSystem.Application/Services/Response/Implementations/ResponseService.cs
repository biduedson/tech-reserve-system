using TechReserveSystem.Application.Services.Responses.Interfaces;
using TechReserveSystem.Shared.Communication.constants;
using TechReserveSystem.Shared.Communication.Response;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Response.Implementations
{
    public class ResponseService<T> : IResponseService<T>
    {
        public Response<T> Success(T data)
        {
            var message = ResourceAppMessages.GetCommunicationMessage(ResponseMessages.OPERATION_SUCCESS);
            return new Response<T>(true, message, data);
        }
    }
}