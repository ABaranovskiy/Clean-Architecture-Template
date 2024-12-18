namespace ShishByzh.Application.Common.Models;

public class Result
{
    public Result()
    {
    }
    
    private Result(bool succeeded, string[] data, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }
    public string[] Data { get; init; }
    public string[] Errors { get; init; }

    public static Result Success(string[] data)
    {
        return new Result(true, data, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, Array.Empty<string>(), errors);
    }
}
