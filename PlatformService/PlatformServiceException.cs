using System.Runtime.Serialization;

namespace PlatformService;

public class PlatformServiceException : Exception
{
    public PlatformServiceException()
    {
    }

    public PlatformServiceException(string? message) : base(message)
    {
    }

    public PlatformServiceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PlatformServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}