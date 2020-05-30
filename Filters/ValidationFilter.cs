using System.Linq;
using System.Threading.Tasks;
using repopractise.Domain.Dtos.ErrorResponse;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace repopractise.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if(!context.ModelState.IsValid) {
                var errrorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ValidationErrorResponse();

                foreach (var error in errrorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorDto { FieldName = error.Key, Message = subError };

                        errorResponse.Errors.Add(errorModel);
                    }

                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}