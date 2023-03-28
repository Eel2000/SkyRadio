namespace SkyRadio.Domain.Commons;

/// <summary>
/// Base response type used in the whole system.
/// </summary>
/// <typeparam name="TData">Data type that's returned.</typeparam>
public class Response<TData>
{
    public Response(string? message)
    {
        IsSucceed = false;
        Message = message;
    }

    public Response(TData data)
    {
        Data = data;
        IsSucceed = true;
    }

    public Response(bool isSucceed, TData? data)
    {
        Data = data;
        IsSucceed = isSucceed;
    }

    public Response(TData? data, string? message)
    {
        Data = data;
        IsSucceed = true;
        Message = message;
    }

    public Response(bool isSucceed, TData? data, string? message)
    {
        Data = data;
        Message = message;
        IsSucceed = isSucceed;
    }

    public bool IsSucceed { get; set; }
    public TData? Data { get; set; }
    public string? Message { get; set; }

}
