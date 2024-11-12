﻿namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using User = Domain.Entities.User;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.Repository;


    /// <summary>
    /// Repository class for reading user entities with filtering.
    /// </summary>
    public class UserReadFilter : ReadFilterRepository<User>, IUserReadFilter
    {
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        public UserReadFilter(
            CommonDbContext context, 
            ILogService logService,
            IResourceProvider resourceProvider,
            IResourceHandler resourceHandler) : base(context, logService, resourceProvider, resourceHandler)
        {
        }
    }
}
