namespace Infrastructure.Message
{
    using Application.UseCases.ExternalServices;
    using System.Resources;

    public class ResxResourceProvider(ResourceManager resourceManager) : IResourceProvider
    {
        private readonly ResourceManager _resourceManager = resourceManager;

        public string GetMessage(string key)
        {
            return _resourceManager.GetString(key) ?? "Message not found.";
        }
    }
}
