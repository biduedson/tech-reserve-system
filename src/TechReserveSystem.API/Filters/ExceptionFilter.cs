using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechReserveSystem.Shared.Communication.Response.Error;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.NotFound;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;

namespace TechReserveSystem.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MyTechReserveSystemException)
                HandleProjectException(context);
            else if (context.Exception is NotFoundExceptionError)
                HandleNotFoundException(context);
            else
                ThrowUnknownException(context);
        }

        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }

        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            if (context.Exception is NotFoundExceptionError)
            {
                var exception = context.Exception as NotFoundExceptionError;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundObjectResult(new ResponseErrorJson(exception!.Message));
            }

        }

        private void ThrowUnknownException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
    }
}