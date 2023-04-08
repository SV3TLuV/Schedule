namespace Schedule.Application.Features.Base.Queries.Paginated;

public abstract record PaginatedQuery(int Page = 1, int Count = 20);