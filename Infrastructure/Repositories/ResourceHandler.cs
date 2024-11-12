﻿using Application.UseCases.ExternalServices;
using Application.UseCases.Repository;
using Infrastructure.Constants;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ResourceHandler : IResourceHandler
    {
        private readonly IResourceProvider _resourceProvider;
        private Dictionary<string, string> _preloadedResources;

        private ResourceHandler(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _preloadedResources = [];
        }

        public static async Task<ResourceHandler> CreateAsync(IResourceProvider resourceProvider, IEnumerable<string> keys = null)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (!keys.Any())
            {
                throw new ArgumentException(MessageConstants.ResourceHandler.EmptyKeysCollection, nameof(keys));
            }

            var handler = new ResourceHandler(resourceProvider);
            await handler.PreloadResourcesAsync(keys);
            return handler;
        }

        private async Task PreloadResourcesAsync(IEnumerable<string> keys)
        {
            _preloadedResources ??= [];
            if (keys is null)
            {
                var result = await _resourceProvider.GetResourceEntries();
                if (result.IsSuccessful && result.Data != null && result.Data.Any())
                {
                    _preloadedResources = result.Data.ToDictionary(x => x.Name, x => x.Value);
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    var resource = await _resourceProvider.GetMessageValueOrDefault(string.Format(MessageConstants.ResourceHandler.DefaultResource, key));
                    _preloadedResources[key] = resource;
                }
            }
        }


        public string GetResource(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _preloadedResources.TryGetValue(key, out var value) ? value : MessageConstants.ResourceHandler.ResourceNotFound;
        }
    }
}
