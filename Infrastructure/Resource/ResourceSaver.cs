namespace Infrastructure.Resource
{
    using Application.UseCases.CRUD.Resource;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Resource = Domain.Entities.Resource;
    public class ResourceSaver
    {
        private IDatabaseProvider _databaseProvider;
        private IResxProvider _resxProvider;
        private IResourceCreate _resourceCreate;
        private IResourceUpdate _resourceUpdate;

        public ResourceSaver(
            IDatabaseProvider databaseProvider, 
            IResxProvider resxProvider, 
            IResourceCreate resourceCreate,
            IResourceUpdate resourceUpdate
            )
        {
            _databaseProvider= databaseProvider;
            _resxProvider= resxProvider;
            _resourceCreate= resourceCreate;
            _resourceUpdate= resourceUpdate;
        }
        public async Task SynchronizeResxToDatabaseAsync()
        {
            var resultResx = await _resxProvider.GetResourceEntries();
            var resultDB = await _databaseProvider.GetResourceEntries();

            if(resultResx.IsSuccessful && resultDB.IsSuccessful)
            {
                var resourceEntries = resultResx.Data;
                var dbResources = resultDB.Data;
                var dbResourcesDict = dbResources.ToDictionary(r => r.Name, r => r);

                foreach (var entry in resourceEntries)
                {
                    if (dbResourcesDict.TryGetValue(entry.Name, out var dbResource))
                    {
                        // Update only if value differs
                        if (dbResource.Value != entry.Value)
                        {
                            dbResource.Value = entry.Value;
                            dbResource.UpdatedAt = DateTime.UtcNow;
                            await _resourceUpdate.Update(dbResource);
                        }
                    }
                    else
                    {
                        // Insert new resource
                        var newResource = new Resource
                        {
                            Name = entry.Name,
                            Value = entry.Value,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };
                        await _resourceCreate.Create(newResource);
                    }
                }
            }
        }
    }
}
