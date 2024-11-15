namespace Application.UseCases.CRUD.Query.Resource
{
    using Application.UseCases.CRUD.Query.User;

    public interface IResourceQuery:
        IResourceReadFilter,
        IResourceReadFilterCount,
        IResourceReadFilterPage,
        IResourceReadId
    {
    }
}
