using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Exceptions;

[Serializable]
public class AppException : Exception
{
    public AppException() : base() { }
    public AppException(string message) : base(message) { }
    public AppException(string message, Exception e) : base(message, e) { }

    public AppException(IEnumerable<string> extraErrorInfo) : base()
    { ExtraErrorInfo = new List<IEnumerable<string>>() { extraErrorInfo }; }
    public AppException(string message, IEnumerable<string> extraErrorInfo) : base(message)
    { ExtraErrorInfo = new List<IEnumerable<string>>() { extraErrorInfo }; }
    public AppException(string message, IEnumerable<string> extraErrorInfo, Exception e) : base(message, e)
    { ExtraErrorInfo = new List<IEnumerable<string>>() { extraErrorInfo }; }

    public AppException(IEnumerable<IEnumerable<string>> extraErrorInfo) : base()
    { ExtraErrorInfo = extraErrorInfo; }
    public AppException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo) : base(message)
    { ExtraErrorInfo = extraErrorInfo; }
    public AppException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo, Exception e) : base(message, e)
    { ExtraErrorInfo = extraErrorInfo; }


    public virtual int ErrorCode { get; set; } = (int)ExceptionCodes.UnHandeled;
    public virtual int StatusCode { get; set; } = StatusCodes.Status418ImATeapot;
    public virtual IEnumerable<IEnumerable<string>> ExtraErrorInfo { get; set; } = new List<List<string>>();

    public override string ToString()
    {
        if (ExtraErrorInfo != null && ExtraErrorInfo.Any() && ExtraErrorInfo.SelectMany(e => e).Any())
            return $"{nameof(AppException)}: ({StatusCode}) - ({base.Message}) - " +
                $"(\n{string.Join(",\n\t", ExtraErrorInfo.Select(subList => $"({string.Join(", ", subList.Select(err => $"({err})"))})"))}\n)";
        return $"{nameof(AppException)}: ({StatusCode}) - ({base.Message})";
    }
}