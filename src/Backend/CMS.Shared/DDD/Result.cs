namespace CMS.Shared.DDD;

public class Result
{
    public bool IsSuccess { get; }
    public IReadOnlyList<string> Errors { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? [];
    }

    public static Result Success() => new(true);
    public static Result Failure(List<string> errors) => new(false, errors);
    public static Result Failure(string error) => new(false, [error]);

}
public class Result<T> : Result
{
    public T Value { get; }
    private Result(T value, bool isSuccess, List<string> errors) : base(isSuccess, errors)
    {
        Value = value;
    }
    public static Result<T> Success(T value) => new(value, true, null!);
    public new static Result<T> Failure(List<string> errors) => new(default!, false, errors);
    public new static Result<T> Failure(string error) => new(default!, false, [error]);

}
