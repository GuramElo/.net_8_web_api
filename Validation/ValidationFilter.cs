using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Reddit.Middlewares;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace Reddit.Validation
{
    //es davamate
   public sealed class ValidationFilter(ILogger<ValidationFilter> logger) : ActionFilterAttribute
    {
        private readonly ILogger<ValidationFilter> _logger = logger;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new CustomErrorResult
                {
                    Succeeded = false,
                    Errors = context.ModelState.Values.SelectMany(
                                o => o.Errors.Select(
                                    e => e.ErrorMessage))
                });
                _logger.LogError("Validation Errorss");
            }

            base.OnActionExecuting(context);
        }
    }
    public class CustomErrorResult
    {
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
