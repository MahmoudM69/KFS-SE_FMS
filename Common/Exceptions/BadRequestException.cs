using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.Exceptions;

[Serializable]
public class BadRequestException : AppException
{
    public BadRequestException() : base() { }
    public BadRequestException(string message) : base(message) { }
    public BadRequestException(string message, Exception e) : base(message, e) { }
    public BadRequestException(IEnumerable<string> extraErrorInfo) : base(extraErrorInfo) { }
    public BadRequestException(string message, IEnumerable<string> extraErrorInfo) : base(message, extraErrorInfo) { }
    public BadRequestException(string message, IEnumerable<string> extraErrorInfo, Exception e) : base(message, extraErrorInfo, e) { }
    public BadRequestException(IEnumerable<IEnumerable<string>> extraErrorInfo) : base(extraErrorInfo) { }
    public BadRequestException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo) : base(message, extraErrorInfo) { }
    public BadRequestException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo, Exception e) :
        base(message, extraErrorInfo, e)
    { }

    public override string Message => $"The request sent was not valid.{base.ToString()}";
    public override int ErrorCode { get; set; } = (int)ExceptionCodes.BadRequest;
    public override int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
}