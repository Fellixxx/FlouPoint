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
        /// The main method, which serves as the application's entry point.
        /// </summary>
        /// <param name = "args">The command-line arguments.</param>
        private static async Task Main(string[] args)
        {
            // Set up a new service collection for dependency injection
            var services = new ServiceCollection();
            // Configure the necessary services for the application
            ConfigureServices(services);
            // Build a service provider from the configured services
            // This service provider is used to resolve and inject dependencies
            var serviceProvider = services.BuildServiceProvider();
            // Resolve the IGitHubRepositoryService from the service provider
            // IGitHubRepositoryService is used to manage GitHub repositories
            var repositoryService = serviceProvider.GetRequiredService<IGitHubRepositoryService>();
            // Create an instance of RepositoryInfo containing repository details
            var repositoryInfo = new RepositoryInfo("TestRepo", "A test repository", isPrivate: true);
            try
            {
                // Attempt to create a new GitHub repository asynchronously
                await repositoryService.CreateRepositoryAsync(repositoryInfo);
                // Confirm and display that the repository was created successfully
                Console.WriteLine($"Repository '{repositoryInfo.Name}' created successfully!");
            }
            catch (GitHubApiException ex) // Handle specific GitHub API exceptions
            {
                Console.WriteLine($"An error occurred while creating the repository: {ex.Message}");
            }
            catch (CredentialNotFoundException ex) // Handle credential-related exceptions
            {
                Console.WriteLine($"Credential error: {ex.Message}");
            }
            catch (Exception ex) // Handle any unexpected exceptions
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Configures the services required for dependency injection.
        /// </summary>
        /// <param name = "services">The service collection to configure.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            // Register an environment variable provider as a singleton service
            services.AddSingleton<IEnvironmentVariableProvider, EnvironmentVariableProvider>();
            // Register a GitHub credential provider as a singleton service
            services.AddSingleton<ICredentialProvider, GitHubCredentialProvider>();
            // Register the GitHub repository service with its dependencies
            // The service is registered using an HttpClient, allowing for HTTP communication
            services.AddHttpClient<IGitHubRepositoryService, GitHubRepositoryService>();
        // Optionally, you can configure specific HttpClient settings here if necessary
        // Example: Setting a timeout for the HttpClient
        // services.AddHttpClient<IGitHubRepositoryService, GitHubRepositoryService>(client =>
        // {
        //     client.Timeout = TimeSpan.FromSeconds(30);
        // });
        }
    }
}