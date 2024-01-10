using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.Exceptions;

[Serializable]
public class ServerException : AppException
{
    public ServerException() : base() { }
    public ServerException(string message) : base(message) { }
    public ServerException(string message, Exception e) : base(message, e) { }
    public ServerException(IEnumerable<string> extraErrorInfo) : base(extraErrorInfo) { }
    public ServerException(string message, IEnumerable<string> extraErrorInfo) : base(message, extraErrorInfo) { }
    public ServerException(string message, IEnumerable<string> extraErrorInfo, Exception e) : base(message, extraErrorInfo, e) { }
    public ServerException(IEnumerable<IEnumerable<string>> extraErrorInfo) : base(extraErrorInfo) { }
    public ServerException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo) : base(message, extraErrorInfo) { }
    public ServerException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo, Exception e) :
        base(message, extraErrorInfo, e)
    { }

    public override string Message => "A server error has occurred.";
    public override int ErrorCode { get; set; } = (int)ExceptionCodes.Server;
    public override int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
}