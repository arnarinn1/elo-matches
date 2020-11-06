namespace EloMatches.Domain.SeedWork
{
    public interface IAggregateRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}