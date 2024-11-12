﻿namespace Application.UseCases.Repository.Status.Status
{
    using Application.Result;
    using Application.UseCases.Repository.Status.StatusChange;

    public interface IUserStatus : IStatusRepository
    {
        Task<Operation<bool>> Activate(string id);
        Task<Operation<bool>> Deactivate(string id);
    }
}
