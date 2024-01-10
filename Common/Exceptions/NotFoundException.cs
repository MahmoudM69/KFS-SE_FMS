using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.Exceptions;

[Serializable]
public class NotFoundException : AppException
{
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception e) : base(message, e) { }
    public NotFoundException(IEnumerable<string> extraErrorInfo) : base(extraErrorInfo) { }
    public NotFoundException(string message, IEnumerable<string> extraErrorInfo) : base(message, extraErrorInfo) { }
    public NotFoundException(string message, IEnumerable<string> extraErrorInfo, Exception e) : base(message, extraErrorInfo, e) { }
    public NotFoundException(IEnumerable<IEnumerable<string>> extraErrorInfo) : base(extraErrorInfo) { }
    public NotFoundException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo) : base(message, extraErrorInfo) { }
    public NotFoundException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo, Exception e) :
        base(message, extraErrorInfo, e)
    { }

    public override string Message => "The requested item was not found.";
    public override int ErrorCode { get; set; } = (int)ExceptionCodes.NotFound;
    public override int StatusCode { get; set; } = StatusCodes.Status404NotFound;
}