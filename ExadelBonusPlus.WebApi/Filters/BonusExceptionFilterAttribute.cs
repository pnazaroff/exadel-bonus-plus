using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExadelBonusPlus.WebApi
{
    public class BonusExceptionFilterAttribute: Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                switch (context.Exception)
                {
                    case InvalidOperationException error:
                        context.Result = new BadRequestObjectResult(error.Message);
                        break;
                    case ArgumentException error:
                        context.Result = new BadRequestObjectResult(error.Message);
                        break;
                    case Exception error:
                        context.Result = new ObjectResult(error.Message){StatusCode = 500};
                        break;
                }
            }

            return Task.FromResult<Object>(null);
        }
    }
}
