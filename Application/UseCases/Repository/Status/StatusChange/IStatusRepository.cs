namespace Application.UseCases.Repository.Status.StatusChange
{
    /// <summary>
    /// Defines the contract for managing the activation and deactivation status of entities in the repository.
    /// This interface inherits from both IActivateRepository and IDeactivateRepository to provide a unified interface for status management.
    /// </summary>
    public interface IStatusRepository : IActivateRepository, IDeactivateRepository
    {
        // This interface is a composite of IActivateRepository and IDeactivateRepository.
        // Additional methods for managing status could be added here in the future.
    }
}
