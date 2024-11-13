﻿namespace Infrastructure.Resource
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
                return entries.ToResultWithXType<Resource>();
            }

            var resources = entries?.Data?.Where(r => r.Name == key).ToList() ?? [];

            if (!resources.Any())
            {
                return OperationBuilder<Resource>.FailBusiness(Message.ResourceProvider.KeyNotFound);
            }

            if (resources.Count > 1)
            {
                return OperationBuilder<Resource>.FailBusiness(Message.ResourceProvider.MultipleWithSameKey);
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
                return OperationBuilder<IQueryable<Resource>>.FailBusiness(Message.ResourceProvider.UnableToReadFile);
            }

            if (entries.Keys.Count == 0)
            {
                return OperationBuilder<IQueryable<Resource>>.FailBusiness(Message.ResourceProvider.KeyNotFound);
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