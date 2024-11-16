namespace Infrastructure.Repositories
{
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Constants;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles resource loading and retrieval operations by interfacing with an external resource provider.
    /// Provides functionalities to preload resources and fetch individual resource values.
    /// </summary>
    public class ResourceHandler : IResourceHandler
    {
        // The resource provider used to fetch resources, injected via constructor.
        private readonly IResourcesProvider _resourceProvider;
        // Internal dictionary to store preloaded resources with their keys.
        private Dictionary<string, string> _preloadedResources;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceHandler"/> class with a specified resource provider.
        /// Ensures that the provided resource provider is not null.
        /// </summary>
        /// <param name = "resourceProvider">The resource provider to be used by the handler.</param>
        /// <exception cref = "ArgumentNullException">Thrown when <paramref name = "resourceProvider"/> is null.</exception>
        private ResourceHandler(IResourcesProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _preloadedResources = new Dictionary<string, string>();
        }

        /// <summary>
        /// Asynchronously creates an instance of <see cref = "ResourceHandler"/> and preloads resources identified by the specified keys.
        /// Validates the keys parameter to ensure it is neither null nor empty before proceeding.
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
        /// If keys are null, attempts to load all available resources.
        /// </summary>
        /// <param name = "keys">A collection of keys to load resources for; if null, all resources are preloaded.</param>
        private async Task PreloadResourcesAsync(IEnumerable<string> keys)
        {
            // Ensure the preloaded resources dictionary is initialized.
            _preloadedResources ??= new Dictionary<string, string>();
            if (keys is null)
            {
                // Attempt to preload all resources if no specific keys are provided.
                var result = await _resourceProvider.GetResourceEntries();
                if (result.IsSuccessful && result.Data != null && result.Data.Any())
                {
                    _preloadedResources = result.Data.ToDictionary(x => x.Name, x => x.Value);
                }
            }
            else
            {
                // Preload resources for each specified key.
                foreach (var key in keys)
                {
                    var resource = await _resourceProvider.GetMessageValueOrDefault(string.Format(Message.ResourceHandler.DefaultResource, key));
                    _preloadedResources[key] = resource;
                }
            }
        }

        /// <summary>
        /// Retrieves a resource value associated with the specified key.
        /// Provides a default "resource not found" message if the key does not exist in preloaded resources.
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