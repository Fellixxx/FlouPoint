using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPointCLI
{
    internal class GitHubCredentials
    {
        public GitHubCredentials(string username, string token)
        {
            Username = username;
            Token = token;
        }

        public string Username { get; set; }
        public string Token { get; set; }
    }
}
