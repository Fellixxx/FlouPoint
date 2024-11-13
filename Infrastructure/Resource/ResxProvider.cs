namespace Infrastructure.Resource
{
    using Application.Result;
    using Application.UseCases.ExternalServices.Resorces;
    using Domain.Entities;
    using Infrastructure.Constants;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;

    public class ResxProvider : IResorcesProvider
    {
        public static Dictionary<string, string> GetEntries()
        {
            var resourceEntries = new Dictionary<string, string>();
            var currentAssembly = Assembly.GetExecutingAssembly();
            var resources = currentAssembly.GetManifestResourceNames();
            var embeddedResources = resources.Where(name => IsResource(name)).ToArray();
            foreach (var resourceName in embeddedResources)
            {
                using Stream resxStream = GetResxStream(currentAssembly, resourceName);
                using ResourceReader reader = new ResourceReader(resxStream);
                foreach (DictionaryEntry entry in reader)
                {
                    if (entry.Key is string entryKey)
                    {
                        var unique = $"{resourceName}.{entryKey}";
                        if (unique != null && entry.Value is string value && !resourceEntries.ContainsKey(unique))
                        {
                            resourceEntries.Add(unique, value);
                        }
                    }
                }
            }

            return resourceEntries;
        }

        private static Stream? GetResxStream(Assembly currentAssembly, string resourceName)
        {
            return currentAssembly.GetManifestResourceStream(resourceName);
        }

        private static bool IsResource(string name)
        {
            return name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<Operation<Resource>> GetMessage(string key)
        {
            var entries = await GetResourceEntries();
            if (!entries.IsSuccessful)
            {
                return entries.ConvertTo<Resource>();
            }

            var resources = entries?.Data?.Where(r => r.Name == key).ToList() ?? [];

            if (!resources.Any())
            {
                var business = new BusinessStrategy<Resource>();
                var keyNotFound = Message.ResourceProvider.KeyNotFound;
                return OperationStrategy<Resource>.Fail(keyNotFound, business);
            }

            if (resources.Count > 1)
            {
                var business = new BusinessStrategy<Resource>();
                var multipleWithSameKey = Message.ResourceProvider.MultipleWithSameKey;
                return OperationStrategy<Resource>.Fail(multipleWithSameKey, business);
            }

            return Operation<Resource>.Success(resources.FirstOrDefault());
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
            var entries = GetEntries();
            if (entries is null)
            {
                var business = new BusinessStrategy<IQueryable<Resource>>();
                var unableToReadFile = Message.ResourceProvider.UnableToReadFile;
                return OperationStrategy<IQueryable<Resource>>.Fail(unableToReadFile, business);
            }

            if (entries.Keys.Count == 0)
            {
                var business = new BusinessStrategy<IQueryable<Resource>>();
                var keyNotFound = Message.ResourceProvider.KeyNotFound;
                return OperationStrategy<IQueryable<Resource>>.Fail(keyNotFound, business);
            }


            var resourceEntries = entries.Select(entry => new Resource
            {
                Id = Guid.NewGuid().ToString(),
                Name = entry.Key,
                Value = entry.Value,
                Comment = string.Empty,
                Active = true
            }).AsQueryable();

            return Operation<IQueryable<Resource>>.Success(resourceEntries);
        }
    }
}
