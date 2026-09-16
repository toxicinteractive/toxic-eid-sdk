using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Toxic.EId.Sdk.SampleApp.Models;

public class TrapExceptionsFilter : IExceptionFilter
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public TrapExceptionsFilter(ProblemDetailsFactory problemDetailsFactory)
    {
        _problemDetailsFactory = problemDetailsFactory;
    }

    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(
            _problemDetailsFactory.CreateProblemDetails(context.HttpContext,
            (int)HttpStatusCode.InternalServerError, 
            context.Exception.Message))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}
