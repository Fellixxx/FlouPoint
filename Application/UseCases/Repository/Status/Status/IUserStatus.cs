namespace Application.UseCases.Repository.Status.Status
{
    using Application.Result;
    using Application.UseCases.Repository.Status.StatusChange;

    public interface IUserStatus : IStatusRepository
    {
        Task<OperationResult<bool>> Activate(string id);
        Task<OperationResult<bool>> Deactivate(string id);
    }
}
