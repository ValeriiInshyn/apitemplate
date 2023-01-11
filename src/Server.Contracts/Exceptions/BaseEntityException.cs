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

public abstract class BaseEntityException<TEntity> : ApiException where TEntity : class
{
    protected BaseEntityException(string message, int statusCode, LogEventLevel logEventLevel) : base(message,
        statusCode, logEventLevel)
    {
    } 
}