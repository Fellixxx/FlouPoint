namespace Infrastructure.Message
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.ResourceEntry;
    using Domain.Entities;
    using Infrastructure.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging.Abstractions;

    public class DatabaseResourceProvider : IResourceProvider
    {
        
        private readonly DbContext _context;
        private readonly DbSet<ResourceEntry> _dbSet;
        private readonly IResourceEntryQuery _resourceEntryQuery;

        public DatabaseResourceProvider(DbContext context, IResourceEntryQuery resourceEntryQuery)
        {
            _context = context;
            _resourceEntryQuery = resourceEntryQuery;
        }

        public async Task<OperationResult<ResourceEntry>> GetMessage(string key)
        {
            var resource = _resourceEntryQuery.ReadFilter(r => r.Name.Equals(key));
            var resources = await GetResourceEntries();
            if (!resources.IsSuccessful)
            {
                return resources.ToResultWithXType<ResourceEntry>();
            }
            if (!resources.Data.Any())
            {
                return OperationBuilder<ResourceEntry>.FailureBusinessValidation(ExceptionMessages.DBResorceProvider.KeyNotFound);
            }

            if (resources.Data.Count() > 1)
            {
                return OperationBuilder<ResourceEntry>.FailureBusinessValidation(ExceptionMessages.DBResorceProvider.MultipleResources);
            }

            return OperationResult<ResourceEntry>.Success(resources.Data.FirstOrDefault());
        }

        public async Task<string> GetMessageValueOrDefault(string key, string defaultValue = ExceptionMessages.DBResorceProvider.KeyNotFound)
        {
            var result = await GetMessage(key);
            if (result.IsSuccessful)
            {
                return result.Data.Value;
            }
            return defaultValue;
        }

        public async Task<OperationResult<IQueryable<ResourceEntry>>> GetResourceEntries()
        {
            var entries = _resourceEntryQuery.ReadFilter(r => r.Active);
            if (entries is null)
            {
                return OperationBuilder<IQueryable<ResourceEntry>>.FailureBusinessValidation(ExceptionMessages.DBResorceProvider.UnableToRead);
            }

            if (entries is not null && entries.Result is not null && entries.Result.Data is not null && !entries.Result.Data.Any())
            {
                return OperationBuilder<IQueryable<ResourceEntry>>.FailureBusinessValidation(ExceptionMessages.DBResorceProvider.NoKeysFound);
            }

            return OperationResult<IQueryable<ResourceEntry>>.Success(entries.Result.Data);
        }
    }
}
