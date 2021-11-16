using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenCMS.Infrastructure.Common
{

    public class CustomValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                var responseObj = new
                {
                    status_code = HttpStatusCode.BadRequest,
                    error_code = 0,
                    validation_error = errors
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 200
                };
            }
        }
    }
}
