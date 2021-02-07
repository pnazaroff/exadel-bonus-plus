using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExadelBonusPlus.WebApi
{
    public class ExceptionFilterAttribute: Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                switch (context.Exception)
                {
                    case InvalidOperationException error:
                        context.Result = new BadRequestObjectResult(new ResultDto<object>()
                        {
                            Errors = new List<string>()
                            {
                                error.Message
                            }
                        });
                        break;
                    case ArgumentException error:
                        context.Result = new BadRequestObjectResult(new ResultDto<object>()
                        {
                            Errors = new List<string>()
                            {
                                error.Message
                            }
                        });
                        break;
                    case ValidationException error:
                        context.Result = new BadRequestObjectResult(new ResultDto<object>()
                        {
                            Errors = error.ValidationErrors
                        });
                        break;
                    default:
                        context.Result = new ObjectResult(new ResultDto<object>()
                        {
                            Errors = new List<string>()
                            {
                                context.Exception.Message
                            }
                        }) {StatusCode = 500};
                        break;
                }
            }

            return Task.FromResult<Object>(null);
        }
    }
}
