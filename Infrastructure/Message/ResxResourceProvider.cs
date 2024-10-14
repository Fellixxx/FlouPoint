
using Application.UseCases.ExternalServices;
using System.Resources;

namespace Infrastructure.Message
{
    public class ResxResourceProvider(ResourceManager resourceManager) : IResourceProvider
    {
        private readonly ResourceManager _resourceManager = resourceManager;

        public string GetMessage(string key)
        {
            return _resourceManager.GetString(key) ?? "Message not found.";
        }
    }
}
