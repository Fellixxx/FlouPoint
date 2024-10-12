using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FlouPoint.GitHub;

namespace FlouPoint.GitHubApp
{
    /// <summary>
    /// The entry point of the GitHub repository management application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method, which is the application's entry point.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        private static async Task Main(string[] args)
        {
            // Set up dependency injection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Resolve the IGitHubRepositoryService and use it
            var repositoryService = serviceProvider.GetRequiredService<IGitHubRepositoryService>();

            // Create a new repository
            var repositoryInfo = new RepositoryInfo("TestRepo", "A test repository", isPrivate: true);

            try
            {
                await repositoryService.CreateRepositoryAsync(repositoryInfo);
                Console.WriteLine($"Repository '{repositoryInfo.Name}' created successfully!");
            }
            catch (GitHubApiException ex)
            {
                Console.WriteLine($"An error occurred while creating the repository: {ex.Message}");
            }
            catch (CredentialNotFoundException ex)
            {
                Console.WriteLine($"Credential error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Configures the services for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            // Register environment variable provider
            services.AddSingleton<IEnvironmentVariableProvider, EnvironmentVariableProvider>();

            // Register credential provider
            services.AddSingleton<ICredentialProvider, GitHubCredentialProvider>();

            // Register the GitHub repository service with an HttpClient
            services.AddHttpClient<IGitHubRepositoryService, GitHubRepositoryService>();

            // Optionally, configure HttpClient settings here if needed
            // services.AddHttpClient<IGitHubRepositoryService, GitHubRepositoryService>(client =>
            // {
            //     client.Timeout = TimeSpan.FromSeconds(30);
            // });
        }
    }
}
