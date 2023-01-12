#region

using Serilog.Events;

#endregion

namespace Server.Contracts.Exceptions;

public abstract class BaseEntityException<TEntity> : ApiException where TEntity : class
{
    protected BaseEntityException(string message, int statusCode, LogEventLevel logEventLevel) : base(message,
        statusCode, logEventLevel)
    {
    }
}