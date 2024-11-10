using Application.UseCases.ExternalServices;

namespace Infrastructure
{
    public abstract class ResourceAwareBase
    {
        protected readonly IResourceProvider _resourceProvider;
        protected readonly Dictionary<string, string> _preloadedResources;

        protected ResourceAwareBase(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider;
            _preloadedResources = new Dictionary<string, string>();
            PreloadResourcesAsync().Wait();
        }

        // Método abstracto que cada clase derivada debe implementar para definir los recursos necesarios
        protected abstract IEnumerable<string> GetResourceKeysToPreload();

        // Método que se encarga de precargar los recursos especificados por la clase derivada
        private async Task PreloadResourcesAsync()
        {
            var keysToPreload = GetResourceKeysToPreload();

            foreach (var key in keysToPreload)
            {
                _preloadedResources[key] = await _resourceProvider.GetMessageValueOrDefault(key, $"Default for {key}");
            }
        }

        // Método para obtener un recurso precargado
        protected string GetPreloadedResource(string key)
        {
            return _preloadedResources.TryGetValue(key, out var value) ? value : "Resource not found";
        }
    }
}
