namespace TransPorter.Shared.Wrapper;

public class Result : IResult
{
    public Result()
    {
    }

    public Result(bool succeeded, params string[] messages)
    {
        Succeeded = succeeded;
        Messages = messages.ToList();
    }

    public List<string> Messages { get; set; } = new List<string>();

    public bool Succeeded { get; set; }

    public void EnsureSucceeded()
    {
        if (!Succeeded)
            throw new ResultException { Messages = Messages };
    }

    public static IResult Fail()
    {
        return new Result { Succeeded = false };
    }

    public static IResult Fail(string message)
    {
        return new Result { Succeeded = false, Messages = new List<string> { message } };
    }

    public static IResult Fail(List<string> messages)
    {
        return new Result { Succeeded = false, Messages = messages };
    }

    public static Task<IResult> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public static Task<IResult> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public static Task<IResult> FailAsync(List<string> messages)
    {
        return Task.FromResult(Fail(messages));
    }

    public static IResult Success()
    {
        return new Result { Succeeded = true };
    }

    public static IResult Success(string message)
    {
        return new Result { Succeeded = true, Messages = new List<string> { message } };
    }

    public static Task<IResult> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<IResult> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }
}

public class Result<T> : Result, IResult<T>
{
    public Result()
    {
    }

    public T Data { get; set; }

    public new static Result<T> Fail()
    {
        return new Result<T> { Succeeded = false };
    }

    public new static Result<T> Fail(string message)
    {
        return new Result<T> { Succeeded = false, Messages = new List<string> { message } };
    }

    public new static Result<T> Fail(List<string> messages)
    {
        return new Result<T> { Succeeded = false, Messages = messages };
    }

    public new static Task<Result<T>> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public new static Task<Result<T>> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public new static Task<Result<T>> FailAsync(List<string> messages)
    {
        return Task.FromResult(Fail(messages));
    }

    public new static Result<T> Success() => new() { Succeeded = true };

    public new static Result<T> Success(string message) 
        => new() { Succeeded = true, Messages = new List<string> { message } };

    public static Result<T> Success(T data) => new() { Succeeded = true, Data = data };

    public static Result<T> Success(T data, string message) 
        => new()
        { Succeeded = true, Data = data, Messages = new List<string> { message } };

    public static Result<T> Success(T data, List<string> messages) 
        => new() { Succeeded = true, Data = data, Messages = messages };

    public new static Task<Result<T>> SuccessAsync() => Task.FromResult(Success());

    public new static Task<Result<T>> SuccessAsync(string message) => Task.FromResult(Success(message));

    public static Task<Result<T>> SuccessAsync(T data) => Task.FromResult(Success(data));

    public static Task<Result<T>> SuccessAsync(T data, string message) => Task.FromResult(Success(data, message));
}

public class ResultException : Exception
{
    public IEnumerable<string>? Messages { get; set; }
}
