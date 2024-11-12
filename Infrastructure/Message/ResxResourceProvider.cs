﻿namespace Infrastructure.Message
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Domain.Entities;
    using Infrastructure.Constants;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;

    public class ResxResourceProvider : IResourceProvider
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

        public async Task<OperationResult<ResourceEntry>> GetMessage(string key)
        {
            var entries = await GetResourceEntries();
            if (!entries.IsSuccessful)
            {
                return entries.ToResultWithXType<ResourceEntry>();
            }

            var resources = entries?.Data?.Where(r => r.Name == key).ToList() ?? [];

            if (!resources.Any())
            {
                return OperationBuilder<ResourceEntry>.FailBusiness(MessageConstants.ResourceProvider.KeyNotFound);
            }

            if (resources.Count > 1)
            {
                return OperationBuilder<ResourceEntry>.FailBusiness(MessageConstants.ResourceProvider.MultipleResourcesWithSameKey);
            }

            return OperationResult<ResourceEntry>.Success(resources.FirstOrDefault());
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
        public async Task<OperationResult<IQueryable<ResourceEntry>>> GetResourceEntries()
        {
            var entries = GetEntries();
            if (entries is null)
            {
                return OperationBuilder<IQueryable<ResourceEntry>>.FailBusiness(MessageConstants.ResourceProvider.UnableToReadResourceFile);
            }

            if (entries.Keys.Count == 0)
            {
                return OperationBuilder<IQueryable<ResourceEntry>>.FailBusiness(MessageConstants.ResourceProvider.KeyNotFound);
            }


            var resourceEntries = entries.Select(entry => new ResourceEntry
            {
                Id = Guid.NewGuid().ToString(),
                Name = entry.Key,
                Value = entry.Value,
                Comment = string.Empty,
                Active = true
            }).AsQueryable();

            return OperationResult<IQueryable<ResourceEntry>>.Success(resourceEntries);
        }
    }
}
