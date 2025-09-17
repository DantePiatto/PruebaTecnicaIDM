namespace PruebaTecnica.Domain.Abstractions;

public interface IUnitOfWorkApplication
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IUnitOfWorkTenant
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}