using Application.UseCases.CRUD.Query.User;
using Application.UseCases.ExternalServices.Resources;
using Application.UseCases.ExternalServices;
using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.UseCases.CRUD.Query.User;
    using System.Linq.Expressions;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterPage;

    /// <summary>
    /// Provides implementation for reading, filtering, and paginating Resource entities.
    /// </summary>
    /// <remarks>
    /// This class extends the functionality provided by <see cref = "ReadFilterPageRepository{TEntity}"/>.
    /// </remarks>
    public class ResourceReadFilterPage : ReadFilterPageRepository<Resource>, IResourceReadFilterPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceReadFilterPage"/> class.
        /// </summary>
        /// <param name = "context">The database context used for data operations.</param>
        /// <param name = "logService">Service used for logging purposes.</param>
        /// <param name = "provider">Provider for external resource services.</param>
        public ResourceReadFilterPage(DataContext context, ILogService logService, IResourcesProvider provider) : base(context, logService, provider)
        {
        }

        /// <summary>
        /// Gets the predicate function used to filter Resource entities based on a key.
        /// </summary>
        /// <param name = "key">The key used for filtering resources. Typically this is the resource name.</param>
        /// <returns>An expression that represents the predicate.</returns>
        public override Expression<Func<Resource, bool>> GetPredicate(string key)
        {
            // Predicate expression to filter resources where the name matches the given key.
            return u => u.Name == key;
        }
    }
}