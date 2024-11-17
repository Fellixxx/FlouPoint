namespace Infrastructure.Resource
{
    using Application.Result;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Domain.Entities;
    using Infrastructure.Constants;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Provides functionality to manage and retrieve resources embedded in the assembly.
    /// </summary>
    public class ResxProvider : IResourcesProvider, IResxProvider
    {
        /// <summary>
        /// Retrieves all entries from the embedded resource files within the assembly.
        /// </summary>
        /// <returns>A dictionary containing resource names as keys and their values as values.</returns>
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

        /// <summary>
        /// Retrieves a stream of the specified resource file from the assembly.
        /// </summary>
        /// <param name = "currentAssembly">The assembly containing the resource.</param>
        /// <param name = "resourceName">The name of the resource.</param>
        /// <returns>A stream representing the resource content, or null if the resource is not found.</returns>
        private static Stream? GetResxStream(Assembly currentAssembly, string resourceName)
        {
            return currentAssembly.GetManifestResourceStream(resourceName);
        }

        /// <summary>
        /// Determines whether a given resource name corresponds to a valid resource file.
        /// </summary>
        /// <param name = "name">The name of the resource.</param>
        /// <returns>True if the name ends with ".resources"; otherwise, false.</returns>
        private static bool IsResource(string name)
        {
            return name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Retrieves a specific resource identified by the provided key.
        /// </summary>
        /// <param name = "key">The key of the resource to retrieve.</param>
        /// <returns>An operation result containing the resource if found or an error if not.</returns>
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

        /// <summary>
        /// Attempts to retrieve the value of a specific resource by its key, returning a default value if not found.
        /// </summary>
        /// <param name = "key">The key of the resource.</param>
        /// <param name = "defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The resource value if found; otherwise, the specified default value.</returns>
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
        /// Retrieves all resource entries as an IQueryable of Resource objects.
        /// </summary>
        /// <returns>An operation result containing the resource entries or an error if the retrieval fails.</returns>
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

            var resourceEntries = entries.Select(entry => new Resource { Id = Guid.NewGuid().ToString(), Name = entry.Key, Value = entry.Value, Comment = string.Empty, Active = true }).AsQueryable();
            return Operation<IQueryable<Resource>>.Success(resourceEntries);
        }
    }
}