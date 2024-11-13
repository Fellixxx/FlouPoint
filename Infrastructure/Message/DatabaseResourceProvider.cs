namespace Infrastructure.Message
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Resource;
    using Domain.Entities;
    using Infrastructure.Constants;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging.Abstractions;

    public class DatabaseResourceProvider : IResourceProvider
    {
        
        private readonly DbContext _context;
        private readonly DbSet<Resource> _dbSet;
        private readonly IQuery _resourceEntryQuery;

        public DatabaseResourceProvider(DbContext context, IQuery resourceEntryQuery)
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
                return resources.ToResultWithXType<Resource>();
            }
            if (!resources.Data.Any())
            {
                return OperationBuilder<Resource>.FailBusiness(MessageConstants.ResourceProvider.KeyNotFound);
            }

            if (resources.Data.Count() > 1)
            {
                return OperationBuilder<Resource>.FailBusiness(MessageConstants.ResourceProvider.MultipleWithSameKey);
            }

            return Operation<Resource>.Success(resources.Data.FirstOrDefault());
        }

        public async Task<string> GetMessageValueOrDefault(string key, string defaultValue = MessageConstants.ResourceProvider.KeyNotFound)
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
                return OperationBuilder<IQueryable<Resource>>.FailBusiness(MessageConstants.ResourceProvider.UnableToReadFile);
            }

            if (entries is not null && entries.Result is not null && entries.Result.Data is not null && !entries.Result.Data.Any())
            {
                return OperationBuilder<IQueryable<Resource>>.FailBusiness(MessageConstants.ResourceProvider.KeyNotFound);
            }

            return Operation<IQueryable<Resource>>.Success(entries.Result.Data);
        }
    }
}
