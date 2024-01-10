using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Common.Exceptions;

[Serializable]
public class NotImplementedException : AppException
{
    public NotImplementedException() : base() { }
    public NotImplementedException(string message) : base(message) { }
    public NotImplementedException(string message, Exception e) : base(message, e) { }
    public NotImplementedException(IEnumerable<string> extraErrorInfo) : base(extraErrorInfo) { }
    public NotImplementedException(string message, IEnumerable<string> extraErrorInfo) : base(message, extraErrorInfo) { }
    public NotImplementedException(string message, IEnumerable<string> extraErrorInfo, Exception e) : base(message, extraErrorInfo, e) { }
    public NotImplementedException(IEnumerable<IEnumerable<string>> extraErrorInfo) : base(extraErrorInfo) { }
    public NotImplementedException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo) : base(message, extraErrorInfo) { }
    public NotImplementedException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo, Exception e) : base(message, extraErrorInfo, e) { }

    public override string Message => "Some required services were not implemented.";
    public override int ErrorCode { get; set; } = (int)ExceptionCodes.NotImplemented;
    public override int StatusCode { get; set; } = StatusCodes.Status501NotImplemented;
}
