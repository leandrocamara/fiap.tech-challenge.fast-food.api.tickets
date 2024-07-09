using Adapters.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Routers;

public abstract class BaseRouter : ControllerBase
{
    protected IActionResult HttpResponse(Result result)
    {
        return result.Type switch
        {
            Result.ResultType.Success => Ok(result.Value),
            Result.ResultType.Created => StatusCode(StatusCodes.Status201Created, result.Value),
            Result.ResultType.Accepted => Accepted(),
            Result.ResultType.Invalid => BadRequest(result.Error),
            Result.ResultType.NotFound => NotFound(),
            Result.ResultType.Failed => StatusCode(StatusCodes.Status500InternalServerError, result.Error),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}