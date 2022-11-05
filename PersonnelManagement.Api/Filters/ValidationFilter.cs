using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonnelManagement.Contracts.v1.Responses;

namespace PersonnelManagement.Server.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(
                        k => k.Key,
                        val => val.Value.Errors.Select(x => x.ErrorMessage)
                    ).ToArray();

                var errorResponse = new ErrorResponse
                {
                    Errors = errors.SelectMany(error =>
                        error.Value.Select(errorValue =>
                            new ErrorModel { FieldName = error.Key, Message = errorValue }
                        )
                    ).ToList()
                };

                context.Result = new BadRequestObjectResult(errorResponse);

                return;
            }

            await next();
        }
    }
}
