namespace Server.Contracts.Responses;

public record PagedResponse<TEntity>(List<TEntity> Data, int TotalCount);