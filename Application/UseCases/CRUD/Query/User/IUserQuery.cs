namespace Application.UseCases.CRUD.Query.User
{
    /// <summary>
    /// The IUserRepository interface defines the contract for a User repository.
    /// This interface extends IRepositoryOperations<User>, meaning it inherits all the CRUD and other operations 
    /// defined in IRepositoryOperations.
    /// In addition, it includes user-specific operations like Login.
    /// </summary>
    public interface IUserQuery : IUserReadFilter, IUserReadFilterCount, IUserReadFilterPage, IUserReadId
    {

    }
}
