#region @copyright by IntenseLab 2022
// // // /////////////////////////////////////////////////////////////////////////////////////
// // // Product name: B2C-QuoteMedia-Data-Services
// // // Product short name: B2C-QM-Admin
// // // Vendor: IntenseLab LLC
// // // License: IntenseLab License
// // // Vendor mail: info@intenselab.com
// // //
// // // Product version: v1.0.1.100
// // // Product description: www.intenselab.com/go/en/solutions
// // // /////////////////////////////////////////////////////////////////////////////////////
#endregion

using Serilog.Events;

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