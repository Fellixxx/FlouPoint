namespace Infrastructure.Resource
{
    using Application.Result;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.Repository.CRUD.Resource;
    using Domain.Entities;
    using Infrastructure.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging.Abstractions;

    public class DatabaseProvider : IResourcesProvider
    {
        
        private readonly DbContext _context;
        private readonly DbSet<Resource> _dbSet;
        private readonly IQuery _resourceEntryQuery;

        public DatabaseProvider(DbContext context, IQuery resourceEntryQuery)
        {
            _context = context;
            _resourceEntryQuery = resourceEntryQuery;
        }

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

        public async Task<string> GetMessageValueOrDefault(string key, string defaultValue = Message.ResourceProvider.KeyNotFound)
        {
            var result = await GetMessage(key);
            if (result.IsSuccessful)
            {
                return result.Data.Value;
            }
            return defaultValue;
        }

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
