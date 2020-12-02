using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Domain.Extensions.Attributes
{
    public class CustomAuthAtt : ResultFilterAttribute
    {
       
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.HttpContext.Request.Headers["Authorization"]);
                if (authHeader != null && authHeader.Parameter == "YWRtaW46YWRtaW4=")
                {
                    base.OnResultExecuting(context);
                }
                else
                {
                    context.Result = new CustomUnauthorizedResult("Authorization failed.");
                    base.OnResultExecuting(context);
                }
            }
            catch (Exception ex)
            {
                context.Result = new CustomUnauthorizedResult("Authorization failed.");
                base.OnResultExecuting(context);
            }


        }

        public class CustomUnauthorizedResult : JsonResult
        {
            public CustomUnauthorizedResult(string message)
                : base(new CustomError(message))
            {
                StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        public class CustomError
        {
            public string Error { get; }

            public CustomError(string message)
            {
                Error = message;
            }
        }

    }
}
