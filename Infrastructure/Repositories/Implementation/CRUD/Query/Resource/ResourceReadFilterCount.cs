using Application.UseCases.CRUD.Query.User;
using Application.UseCases.ExternalServices;
using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using System.Linq.Expressions;
    using Application.UseCases.ExternalServices;
    using Resource = Domain.Entities.Resource;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterCount;
    using Application.UseCases.CRUD.Query.Resource;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Implements the resource-specific read filter count functionality.
    /// Inherits from the generic ReadFilterCountRepository.
    /// </summary>
    public class ResourceReadFilterCount : ReadFilterCountRepository<Resource>, IResourceReadFilterCount
    {
        /// <summary>
        /// Initializes a new instance of the ResourceReadFilterCount class.
        /// </summary>
        /// <param name = "context">Database context for accessing data.</param>
        /// <param name = "logService">Service for logging activities.</param>
        /// <param name = "provider">Provider for accessing resources.</param>
        /// <param name = "handler">Handler for resource operations.</param>
        public ResourceReadFilterCount(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }

        /// <summary>
        /// Constructs a predicate to filter resources by their name.
        /// </summary>
        /// <param name = "key">The key to match against resource names.</param>
        /// <returns>An expression representing the filtering predicate.</returns>
        public override Expression<Func<Resource, bool>> GetPredicate(string key)
        {
            // Returns a lambda expression to filter resources where the Name matches the specified key.
            return u => u.Name == key;
        }
    }
}