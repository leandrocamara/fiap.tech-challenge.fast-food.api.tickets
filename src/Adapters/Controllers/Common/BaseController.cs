using ApplicationException = Application.ApplicationException;

namespace Adapters.Controllers.Common;

public abstract class BaseController
{
    protected async Task<TResponse> Execute<TResponse>(Func<Task<TResponse>> useCase)
    {
        try
        {
            return await useCase();
        }
        catch (ApplicationException e)
        {
            throw new ControllerException(Result.Invalid(e.Message));
        }
        catch (Exception e)
        {
            throw new ControllerException(Result.Failed(e));
        }
    }
}