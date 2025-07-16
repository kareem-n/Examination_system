using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Template.API.Response;

namespace Template.API.Filters
{
    public class ModelValidationFilter : IActionFilter

    {
        private readonly ILogger<ModelValidationFilter> logger;

        public ModelValidationFilter(ILogger<ModelValidationFilter> logger)
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {


            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {

                var errors = context.ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => new
                    {
                        x.Key,
                        Errors = x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    })
                    .ToList();

                logger.BeginScope("Model Validation Errors");
                foreach (var error in errors)
                {
                    logger.LogError("Key: {Key}, Errors: {Errors}", error.Key, string.Join(", ", error.Errors));
                }

                context.Result = new JsonResult(ApiResponse<object>.Error(400, "Validation Errors", errors.ToArray()))
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };

            }

        }


    }
}
