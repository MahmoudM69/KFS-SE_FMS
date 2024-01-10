using System;
using System.Collections.Generic;

namespace Common.Exceptions;

[Serializable]
public class ValidationNotImplementedException<T> : NotImplementedException
{
    public ValidationNotImplementedException() : base() { }
    public ValidationNotImplementedException(string message) : base(message) { }
    public ValidationNotImplementedException(string message, Exception e) : base(message, e) { }
    public ValidationNotImplementedException(IEnumerable<string> extraErrorInfo) : base(extraErrorInfo) { }
    public ValidationNotImplementedException(string message, IEnumerable<string> extraErrorInfo) : base(message, extraErrorInfo) { }
    public ValidationNotImplementedException(string message, IEnumerable<string> extraErrorInfo, Exception e) :
        base(message, extraErrorInfo, e)
    { }
    public ValidationNotImplementedException(IEnumerable<IEnumerable<string>> extraErrorInfo) : base(extraErrorInfo) { }
    public ValidationNotImplementedException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo) :
        base(message, extraErrorInfo)
    { }
    public ValidationNotImplementedException(string message, IEnumerable<IEnumerable<string>> extraErrorInfo, Exception e) :
        base(message, extraErrorInfo, e)
    { }

    public override string Message => $"The validation were not implemented for the {typeof(T).Name} service.";
}
