namespace Infrastructure.Message
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.ResourceEntry;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Resources;

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
                return OperationBuilder<ResourceEntry>.FailureBusinessValidation("No resource exists with the specified key.");
            }

            if (resources.Data.Count() > 1)
            {
                return OperationBuilder<ResourceEntry>.FailureBusinessValidation("Multiple resources exist with the same key.");
            }

            return OperationResult<ResourceEntry>.Success(resources.Data.FirstOrDefault());
        }

        public async Task<OperationResult<IQueryable<ResourceEntry>>> GetResourceEntries()
        {
            var entries = _resourceEntryQuery.ReadFilter(r=>r.Active);
            if (entries is null)
            {
                return OperationBuilder<IQueryable<ResourceEntry>>.FailureBusinessValidation("Unable to read the resources file.");
            }

            if (entries.Result.Data.Count() == 0)
            {
                return OperationBuilder<IQueryable<ResourceEntry>>.FailureBusinessValidation("No resource keys were found.");
            }

            return OperationResult<IQueryable<ResourceEntry>>.Success(entries.Result.Data);
        }
    }
}
