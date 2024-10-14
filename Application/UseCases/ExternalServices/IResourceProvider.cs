namespace Application.UseCases.ExternalServices
{
    public interface IResourceProvider
    {
        string GetMessage(string key);
    }
}
