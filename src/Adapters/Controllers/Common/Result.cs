namespace Adapters.Controllers.Common;

public struct Result
{
    public object? Value { get; }
    public bool IsSuccess { get; }
    public ResultType Type { get; }
    public string? Error { get; }

    private Result(object? value, ResultType type)
    {
        Value = value;
        IsSuccess = true;
        Type = type;
    }

    private Result(ResultType type, string? error = null)
    {
        Value = null;
        IsSuccess = false;
        Type = type;
        Error = error;
    }

    public static Result Success(object value) => new(value, ResultType.Success);
    public static Result Created(object value) => new(value, ResultType.Created);
    public static Result Accepted() => new(null, ResultType.Accepted);
    public static Result Invalid(string error) => new(ResultType.Invalid, error);
    public static Result NotFound() => new(ResultType.NotFound);
    public static Result Failed(Exception e) => new(ResultType.Failed, e.Message);

    public enum ResultType : short
    {
        Success,
        Created,
        Accepted,
        Invalid,
        NotFound,
        Failed
    }
}