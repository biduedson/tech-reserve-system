using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechReserveSystem.Application.Services.Responses.Interfaces;
using TechReserveSystem.Shared.Communication.Response.Error;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.NotFound;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;

namespace TechReserveSystem.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IResponseService<ResponseErrorJson> _responseService;

        public ExceptionFilter(IResponseService<ResponseErrorJson> responseService)
        {
            _responseService = responseService;
        }

        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = GetStatusCode(context.Exception);
            context.Result = GetErrorResponse(context.Exception);
        }

        private int GetStatusCode(Exception exception) =>
            exception switch
            {
                ErrorOnValidationException => (int)HttpStatusCode.BadRequest,
                BusinessException => (int)HttpStatusCode.BadRequest,
                NotFoundExceptionError => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

        private IActionResult GetErrorResponse(Exception exception) =>
            exception switch
            {
                ErrorOnValidationException e => new BadRequestObjectResult(_responseService.Failure(e.ErrorMessages.ToList())),
                BusinessException e => new BadRequestObjectResult(_responseService.Failure(e.Message)),
                NotFoundExceptionError e => new NotFoundObjectResult(_responseService.Failure(e.Message)),
                _ => new ObjectResult(_responseService.Failure(exception.StackTrace ?? "Erro desconhecido"))
            };
    }
}
