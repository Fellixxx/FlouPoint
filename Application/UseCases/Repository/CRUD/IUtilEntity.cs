﻿namespace Application.UseCases.Repository.CRUD
{
    using Application.Result;
    using Domain.Interfaces.Entity;

    public interface IUtilEntity<T> where T : class, IEntity
    {
        Task<Operation<T>> HasEntity(T entity);
    }
}
