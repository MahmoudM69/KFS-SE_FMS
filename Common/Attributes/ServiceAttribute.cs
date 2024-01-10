using System;

namespace Common.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class ServiceAttribute : Attribute
{
    public string Type { get; }

    public ServiceAttribute(string type)
    {
        Type = type;
    }
}
