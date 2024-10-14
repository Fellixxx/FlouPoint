using Application.UseCases.ExternalServices;
using Domain.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Message
{
    public class DatabaseResourceProvider<T> : IResourceProvider where T : class, IEntity, IDescribable
    {
        
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public DatabaseResourceProvider(DbContext context)
        {
            _context = context;
        }
        

        public string GetMessage(string key)
        {

            var resource = _dbSet.FirstOrDefault(r => r.Id == key);
            return resource?.Message ?? "Message not found in database.";
        }
    }
}
