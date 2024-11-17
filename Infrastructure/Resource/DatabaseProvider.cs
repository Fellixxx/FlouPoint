namespace Infrastructure.Resource
{
    using Application.Result;
    using Application.UseCases.CRUD.Query.Resource;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Domain.Entities;
    using Infrastructure.Constants;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides database operations for managing resources.
    /// Implements the IResourcesProvider interface to interact with resources in the database.
    /// </summary>
    public class DatabaseProvider : IResourcesProvider, IDatabaseProvider
    {
        private readonly DbContext _context;
        private readonly IResourceQuery _resourceEntryQuery;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DatabaseProvider"/> class with the specified database context and query service.
        /// </summary>
        /// <param name = "context">The database context to use.</param>
        /// <param name = "resourceEntryQuery">The query service to query resource entries.</param>
        public DatabaseProvider(DbContext context, IResourceQuery resourceEntryQuery)
        {
            _context = context;
            _resourceEntryQuery = resourceEntryQuery;
        }

        /// <summary>
        /// Retrieves a message by its key from the resources.
        /// </summary>
        /// <param name = "key">The key of the resource to retrieve.</param>
        /// <returns>An operation result containing the resource if found, otherwise an error message.</returns>
        public async Task<Operation<Resource>> GetMessage(string key)
        {
            var resource = _resourceEntryQuery.ReadFilter(r => r.Name.Equals(key));
            var resources = await GetResourceEntries();
            if (!resources.IsSuccessful)
            {
                return resources.ConvertTo<Resource>();
            }

            if (!resources.Data.Any())
            {
                var keyNotFound = Message.ResourceProvider.KeyNotFound;
                return OperationStrategy<Resource>.Fail(keyNotFound, new BusinessStrategy<Resource>());
            }

            if (resources.Data.Count() > 1)
            {
                var multipleWithSameKey = Message.ResourceProvider.MultipleWithSameKey;
                return OperationStrategy<Resource>.Fail(multipleWithSameKey, new BusinessStrategy<Resource>());
            }

            return Operation<Resource>.Success(resources.Data.FirstOrDefault());
        }

        /// <summary>
        /// Retrieves a message value by its key from the resources, or returns a default value if the key is not found.
        /// </summary>
        /// <param name = "key">The key of the resource to retrieve.</param>
        /// <param name = "defaultValue">The default value to return if the key is not found.</param>
        /// <returns>The value of the resource if found, otherwise the default value.</returns>
        public async Task<string> GetMessageValueOrDefault(string key, string defaultValue = Message.ResourceProvider.KeyNotFound)
        {
            var result = await GetMessage(key);
            if (result.IsSuccessful)
            {
                return result.Data.Value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Retrieves all active resource entries that are available in the database.
        /// </summary>
        /// <returns>An operation result containing a queryable collection of resources if found, otherwise an error message.</returns>
        public async Task<Operation<IQueryable<Resource>>> GetResourceEntries()
        {
            var entries = _resourceEntryQuery.ReadFilter(r => r.Active);
            if (entries is null)
            {
                var unableToReadFile = Message.ResourceProvider.UnableToReadFile;
                var business = new BusinessStrategy<IQueryable<Resource>>();
                return OperationStrategy<IQueryable<Resource>>.Fail(unableToReadFile, business);
            }

            if (entries is not null && entries.Result is not null && entries.Result.Data is not null && !entries.Result.Data.Any())
            {
                var keyNotFound = Message.ResourceProvider.KeyNotFound;
                var business = new BusinessStrategy<IQueryable<Resource>>();
                return OperationStrategy<IQueryable<Resource>>.Fail(keyNotFound, business);
            }

            return Operation<IQueryable<Resource>>.Success(entries.Result.Data);
        }
    }
}