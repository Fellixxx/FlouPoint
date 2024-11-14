namespace Infrastructure.Repositories
{
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Constants;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles resource loading and retrieval operations by interfacing with an external resource provider.
    /// </summary>
    public class ResourceHandler : IResourceHandler
    {
        private readonly IResourcesProvider _resourceProvider;
        private Dictionary<string, string> _preloadedResources;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceHandler"/> class with a specified resource provider.
        /// </summary>
        /// <param name = "resourceProvider">The resource provider to be used by the handler.</param>
        /// <exception cref = "ArgumentNullException">Thrown if <paramref name = "resourceProvider"/> is null.</exception>
        private ResourceHandler(IResourcesProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _preloadedResources = new Dictionary<string, string>();
        }

        /// <summary>
        /// Asynchronously creates an instance of <see cref = "ResourceHandler"/> and preloads resources identified by the specified keys.
        /// </summary>
        /// <param name = "resourceProvider">The provider used to retrieve resource data.</param>
        /// <param name = "keys">A collection of keys for which the resources are to be preloaded.</param>
        /// <returns>An instance of <see cref = "ResourceHandler"/> with preloaded resources.</returns>
        /// <exception cref = "ArgumentNullException">Thrown if <paramref name = "keys"/> is null.</exception>
        /// <exception cref = "ArgumentException">Thrown if <paramref name = "keys"/> is an empty collection.</exception>
        public static async Task<ResourceHandler> CreateAsync(IResourcesProvider resourceProvider, IEnumerable<string> keys = null)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (!keys.Any())
            {
                throw new ArgumentException(Message.ResourceHandler.EmptyKeysCollection, nameof(keys));
            }

            var handler = new ResourceHandler(resourceProvider);
            await handler.PreloadResourcesAsync(keys);
            return handler;
        }

        /// <summary>
        /// Preloads resources asynchronously from the resource provider using the specified keys.
        /// </summary>
        /// <param name = "keys">A collection of keys to load resources for; if null, all resources are preloaded.</param>
        private async Task PreloadResourcesAsync(IEnumerable<string> keys)
        {
            _preloadedResources ??= new Dictionary<string, string>();
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
                    var resource = await _resourceProvider.GetMessageValueOrDefault(string.Format(Message.ResourceHandler.DefaultResource, key));
                    _preloadedResources[key] = resource;
                }
            }
        }

        /// <summary>
        /// Retrieves a resource value associated with the specified key.
        /// </summary>
        /// <param name = "key">The key of the resource to retrieve.</param>
        /// <returns>The resource value if found; otherwise, a default "resource not found" message.</returns>
        /// <exception cref = "ArgumentNullException">Thrown if <paramref name = "key"/> is null.</exception>
        public string GetResource(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _preloadedResources.TryGetValue(key, out var value) ? value : Message.ResourceHandler.ResourceNotFound;
        }
    }
}