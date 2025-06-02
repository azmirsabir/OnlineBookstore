using System.Net;
using Bookstore.Model.Response;

namespace Bookstore.Exceptions;

public class NotFoundException(string message) : CustomException(HttpStatusCode.NotFound,message) { }