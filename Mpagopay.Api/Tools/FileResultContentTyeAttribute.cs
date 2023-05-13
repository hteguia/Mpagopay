using System;

namespace Mpagopay.Api.Tools
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FileResultContentTyeAttribute : Attribute
    {
        public string ContentType { get; }
        public FileResultContentTyeAttribute(string contentType)
        {
            ContentType = contentType;
        }
    }
}
