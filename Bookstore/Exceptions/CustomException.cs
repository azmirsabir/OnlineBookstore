using System.Net;
using Bookstore.Model.Response;

namespace Bookstore.Exceptions;

public class CustomException: Exception
{
    public int StatusCode { get; }
    public Response<object> Data { get; }

    public CustomException(HttpStatusCode statusCode,string message, Response<object>? data = default)
    {
        StatusCode = (int)statusCode;
        Data = new Response<object>(message, data);
    }
    
    
}