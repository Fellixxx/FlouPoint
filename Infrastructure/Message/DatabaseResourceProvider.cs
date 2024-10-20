using Application.Result;
using Application.UseCases.ExternalServices;
using Application.UseCases.Repository.CRUD.ResourceEntry;
using Domain.Entities;
using Domain.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Message
{
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
        

        public string GetMessage(string key)
        {
            var resource = _resourceEntryQuery.ReadFilter(r => r.Name.Equals(key));
            if(resource.Result.IsSuccessful)
            {
                return resource.Result.Data.FirstOrDefault().Value;
            }

            return null;
        }
    }
}
