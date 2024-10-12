// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FlouPointCLI
{

    class Program
    {
        static async Task Main(string[] args)
        {
            // Replace with your GitHub username and personal access token
            var credentials = GetGitHubCredentials();
            // Repository details
            string repoName = "NewRepository";
            string repoDescription = "This is a new repository created via the GitHub API";
            bool isPrivate = false;

            await CreateGitHubRepository(credentials.Username, credentials.Token, repoName, repoDescription, isPrivate);
        }

        static async Task CreateGitHubRepository(string username, string token, string repoName, string repoDescription, bool isPrivate)
        {
            var client = new HttpClient();

            // GitHub API endpoint for repository creation
            var url = "https://api.github.com/user/repos";

            // Authentication
            var byteArray = System.Text.Encoding.ASCII.GetBytes($"{username}:{token}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            // GitHub requires a User-Agent header
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("CSharpApp");

            // Repository data
            var postData = new
            {
                name = repoName,
                description = repoDescription,
                @private = isPrivate
            };

            var json = JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Repository '{repoName}' created successfully!");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error creating repository:");
                Console.WriteLine(responseBody);
            }
        }

        public static GitHubCredentials GetGitHubCredentials()
        {
            var username = Environment.GetEnvironmentVariable("GITHUB_USERNAME", EnvironmentVariableTarget.User);
            var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN", EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidOperationException("GitHub username is not set in the environment variables.");
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("GitHub token is not set in the environment variables.");
            }

            return new GitHubCredentials(username, token);
        }
    }
}