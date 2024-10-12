namespace Application.UseCases.Repository
{
    using Application.UseCases.Repository.CRUD;
    using Application.UseCases.Repository.Status.StatusChange;
    using Domain.Interfaces.Entity;

    /// <summary>
    /// Defines the contract for a generic repository where T is an entity in the domain.
    /// This interface is a composite of ICreateRepository, IUpdateRepository, IDeleteRepository, and IStatusRepository,
    /// providing a unified interface for CRUD operations and status management.
    /// </summary>
    /// <typeparam name="T">The type of the entity, which must implement IEntity.</typeparam>
    public interface IEntityRepository<T> :
        ICreateRepository<T>,
        IUpdateRepository<T>,
        IDeleteRepository<T>,
        IStatusRepository
        where T : class, IEntity
    {
        // This interface is a composite of CRUD and status management interfaces.
        // Additional methods for managing entities could be added here in the future.
    }
}

