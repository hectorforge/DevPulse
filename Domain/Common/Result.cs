namespace Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string Message { get; }
    public IDictionary<string, string> ErrorsValidations { get; }

    private Result(
        bool isSuccess,
        T? value,
        string message,
        IDictionary<string, string>? errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Message = message;
        ErrorsValidations = errors ?? new Dictionary<string, string>();
    }

    public static Result<T> Success(T value, string message = "")
    {
        return new Result<T>(
            true,
            value,
            message,
            new Dictionary<string, string>()
        );
    }

    public static Result<T> Failure(
        string message,
        IDictionary<string, string>? errors = null)
    {
        return new Result<T>(
            false,
            default,
            message,
            errors
        );
    }
}