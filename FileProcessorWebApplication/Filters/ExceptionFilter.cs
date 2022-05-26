using System;
using FileProcessor.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileProcessor.WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException)
            {
                context.Result = new BadRequestObjectResult(new { @error = context.Exception.Message });
            }
        }
    }
}

