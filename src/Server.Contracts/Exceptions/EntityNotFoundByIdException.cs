#region

using Serilog.Events;

#endregion

namespace Server.Contracts.Exceptions;

public sealed class EntityNotFoundByIdException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
    private readonly object _identifier;

    public EntityNotFoundByIdException(object identifier) : base(
        $@"{typeof(TEntity).Name} with id {identifier} was not found", 404, LogEventLevel.Warning)
    {
        _identifier = identifier;
    }
}