namespace SB.PruebaTecnica.Domain.Abstractions;


public interface IEntity
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}