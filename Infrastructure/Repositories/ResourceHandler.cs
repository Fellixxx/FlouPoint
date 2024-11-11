using Application.UseCases.ExternalServices;
using Application.UseCases.Repository;

namespace Infrastructure.Repositories
{
    public class ResourceHandler : IResourceHandler
    {
        private readonly IResourceProvider _resourceProvider;
        private readonly Dictionary<string, string> _preloadedResources;

        private ResourceHandler(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _preloadedResources = [];
        }

        public static async Task<ResourceHandler> CreateAsync(IResourceProvider resourceProvider, IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (!keys.Any())
            {
                throw new ArgumentException("Keys collection cannot be empty.", nameof(keys));
            }

            var handler = new ResourceHandler(resourceProvider);
            await handler.PreloadResourcesAsync(keys);
            return handler;
        }

        private async Task PreloadResourcesAsync(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                _preloadedResources[key] = await _resourceProvider.GetMessageValueOrDefault(key, $"Default for {key}");
            }
        }

        public string GetResource(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _preloadedResources.TryGetValue(key, out var value) ? value : "Resource not found";
        }
    }
}
