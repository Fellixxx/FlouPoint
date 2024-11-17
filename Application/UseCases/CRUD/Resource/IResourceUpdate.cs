namespace Application.UseCases.CRUD.Resource
{
    using Application.Result;
    using Domain.Entities;


    public interface IResourceUpdate
    {
        Task<Operation<bool>> Update(Resource entity);
    }
}
