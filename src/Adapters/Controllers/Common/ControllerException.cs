namespace Adapters.Controllers.Common;

public class ControllerException(Result result) : Exception
{
    public Result Result { get; } = result;
}